namespace Client.Web.Repositories
{
    public class RepositoryBase
    {
        public IWebClient Client { get; }
        
        public RepositoryBase(IWebClient client)
        {
            this.Client = client;
        }
    }
}