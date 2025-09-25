using System.Collections.Generic;

namespace SaveSystem
{
    public class GameRepository : IGameRepository
    {
        private const string SAVE_FILE_NAME = "GameState.txt";
        
        private Dictionary<string, string> gameState = new();
        
        public bool TryGetData<T>(out T data)
        {
            var key = typeof(T).ToString();
            if (!gameState.ContainsKey(key))
            {
                data = default;
                return false;
            }

            var dataJson = gameState[key];
            //data = JsonConvert.DeserializeObject<T>(dataJson);
            data = default;
            return true;
        }

        public void SetData<T>(T data)
        {
            throw new System.NotImplementedException();
        }
    }
}