using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace Tachufind
{
    public class EngineCacheManager : IDisposable
    {
        private Dictionary<string, TesseractEngine> engineCache = new Dictionary<string, TesseractEngine>();
        private bool isDisposed = false;

        public TesseractEngine GetEngine(string dataPath, string language = "eng")
        {
            if (isDisposed)
            {
                throw new ObjectDisposedException("EngineCacheManager", "The Tesseract engine has already been disposed.");
            }

            string key = $"{dataPath}_{language}";
            if (!engineCache.ContainsKey(key))
            {
                engineCache[key] = new TesseractEngine(dataPath, language, EngineMode.Default);
            }
            return engineCache[key];
        }

        public void Dispose()
        {
            if (isDisposed)
            {
                return;
            }

            foreach (var engine in engineCache.Values)
            {
                engine.Dispose();
            }
            engineCache.Clear();
            isDisposed = true;
        }
    }

}
