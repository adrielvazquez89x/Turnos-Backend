using Microsoft.AspNetCore.SignalR;

public class TurnsHub : Hub
{
    public async Task SendTurn(string turno)
    {
        await Clients.All.SendAsync("ReceiveTurn", turno);
    }
}