using Jose;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static NPOI.HSSF.Util.HSSFColor;

namespace Medical.Utility
{
    public static class DecodeJwt
    {
        public static IConfiguration MyProperty { get; set; }
        // 用于解码token 用到jose-jwt包
        public static string decode(this string tokenToDecode)
        {
            string token = Jose.JWT.Decode(
                tokenToDecode,
                Encoding.UTF8.GetBytes("JWTStudyWebsite_DI20DXU3"),
                JweAlgorithm.PBES2_HS256_A128KW,
                JweEncryption.A128CBC_HS256,		// 加密算法
                null
            );
            Console.WriteLine(token);
            return token;
        }
    }
}
