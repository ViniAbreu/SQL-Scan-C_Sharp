using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace SQL_Scan_by_Biggs
{
    /// <summary>
    /// Class de response da pagina vuneravel!
    /// </summary>
    public class SetURL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Url"> URL PARA SCANNEAR O SITE</param>
        /// <returns> RETORNA O CONTENT DA PAGINA COMPLETO</returns>
        public async Task<System.Net.Http.HttpResponseMessage> responsiveUrl(Uri Url)
        {
            // Bloco Disponse();
            using (var ClientScan = new HttpClient())
            {
                // Retorna o Content
                return await ClientScan.GetAsync(Url);
            }
        }
    }
}
