using System;
using System.Text;
using RabbitMQ.Client;

namespace Medical.Producer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*1、创建连接工厂
            2、通过工厂创建连接
            3、通过连接创建通道
            4、通过通道声明一个队列
            5、*/

            //1、创建连接工厂
            var factory = new ConnectionFactory { HostName = "localhost" };

            //2、通过工厂创建连接
            var connection = factory.CreateConnection();

            //3、通过连接创建通道
            var channel = connection.CreateModel();                       

            for (int i = 0; i < 20; i++)
            {
                var message = $"message_{i}";

                var messageBody = Encoding.UTF8.GetBytes(message);

                //消息的持久化
                var properties = channel.CreateBasicProperties();

                properties.Persistent = true;

                //发布消息
                channel.BasicPublish(exchange: "amqp.fanout.1911", routingKey: "", basicProperties: properties, messageBody);
            }

            Console.WriteLine("发布20条消息");
            Console.ReadLine();

            

            /*Console.WriteLine("+++++++++++++");
            Console.WriteLine("请输入消息");
            Console.WriteLine("+++++++++++++");

            while (true)
            {
                var message = Console.ReadLine();

                var messageBody = Encoding.UTF8.GetBytes(message);

                //发布消息
                channel.BasicPublish(exchange: "", routingKey: "mymsg", basicProperties: null, messageBody);
            }*/
        }
    }
}
