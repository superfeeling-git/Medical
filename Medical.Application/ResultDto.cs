using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Medical.Application
{
    /// <summary>
    /// 接口返回
    /// </summary>
    public class ResultDto
    {
        public HttpStatusCode Code { get; set; }
        public string Msg { get; set; }
    }

    /// <summary>
    /// 接口返回
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResultDto<T>
    {
        public HttpStatusCode Code { get; set; }
        public string Msg { get; set; }
        public T Data { get; set; }
    }
}
