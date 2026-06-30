    namespace Application;

    public interface IAiController
    { 
        void Create();
        string GetResponse(string prompt);
    }