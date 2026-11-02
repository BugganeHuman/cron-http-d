namespace cron_http_d

{
    public class Worker : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                string timeNow = DateTime.Now.ToString("dd.MM.yyyy  HH.mm.ss");
                try
                {
                    string response = await Program.Client.GetStringAsync(Program.Endpoint,
                                                                        cancellationToken);
                    File.AppendAllText("cron-http-d-logs.txt", $"{timeNow}  {response}\n");
                }
                catch (HttpRequestException e)
                {
                    File.AppendAllText("cron-http-d-logs.txt", $"{timeNow} ERROR -  {e}\n");
                }
                await Task.Delay(Program.Interval * 1000, cancellationToken);
            }

        }
    }


}
