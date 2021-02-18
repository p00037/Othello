using Microsoft.AspNetCore.Components;
using Othello.Blazor.Shared;
using Othello.Shared;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Timers;

namespace Othello.Blazor.Client.Pages
{
    public partial class AiAndAiMatch : ComponentBase
    {
        [Inject]
        HttpClient Http { get; set; }

        private BoardState boardState = new BoardState();
        private Timer timer;
        private List<AiInfo> aiInfoList = new List<AiInfo>();
        private string ai1;
        private string ai2;

        protected override async Task OnInitializedAsync()
        {
            this.aiInfoList = await Http.GetFromJsonAsync<List<AiInfo>>("AiList");
        }

        public void ButtonStartClick()
        {
            this.boardState = new BoardState();
            SetTimmer();
        }

        private void SetTimmer()
        {
            // Create a timer with a two second interval.
            timer = new Timer(1000);

            // Hook up the Elapsed event for the timer.
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private async void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            var aiConfig = new AiConfig(boardState.Board, boardState.TurnPiece, GetAiName());
            var result = await Http.PostAsJsonAsync("Ai", aiConfig);
            var boardPoint = await result.Content.ReadFromJsonAsync<BoardPoint>();
            Console.WriteLine($"AiName:{GetAiName()}");
            Console.WriteLine($"X:{boardPoint.X} Y:{boardPoint.Y}");
            boardState.ExecuteTurn(boardPoint);
            StateHasChanged();

            if (boardState.GetPossibleSetStoneCount(Piece.Black) == 0 &&
                boardState.GetPossibleSetStoneCount(Piece.White) == 0)
            {
                timer.Stop();
            }
        }

        private string GetAiName()
        {
            if (boardState.TurnPiece == Piece.Black)
            {
                return ai1;
            }

            return ai2;
        }
    }
}
