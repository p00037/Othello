using Microsoft.AspNetCore.Components;
using Othello.Blazor.Shared;
using Othello.Shared;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Timers;

namespace Othello.Blazor.Client.Pages
{
    public partial class AiAndAiMatch : ComponentBase
    {
        [Inject]
        HttpClient Http { get; set; }

        private BoardState boardState = new BoardState();
        private Timer timer;

        protected override void OnInitialized()
        {
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
            var aiConfig = new AiConfig(boardState.Board, boardState.TurnPiece, "sampleAI");
            var result = await Http.PostAsJsonAsync("Ai", aiConfig);
            var boardPoint = await result.Content.ReadFromJsonAsync<BoardPoint>();
            Console.WriteLine($"X:{boardPoint.X} Y:{boardPoint.Y}");
            boardState.ExecuteTurn(boardPoint);
            StateHasChanged();

            if (boardState.GetPossibleSetStoneCount(Piece.Black) == 0 &&
                boardState.GetPossibleSetStoneCount(Piece.White) == 0)
            {
                timer.Stop();
            }
        }
    }
}
