using System;
using System.Collections;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Medical.Consumer
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

            //4、创建消费者
            var consumer = new EventingBasicConsumer(channel);

            //5、接收消息
            consumer.Received += (sender, args) => {
                //接收消息
                string ReceivedMessage = string.Empty;

                //接收到的数据
                var ReceivedData = args.Body.ToArray();

                //字节数组转字符串
                ReceivedMessage = Encoding.UTF8.GetString(ReceivedData);

                Console.WriteLine("+++++++++++++");
                Console.WriteLine("接收到的消息");
                Console.WriteLine("+++++++++++++");
                Console.WriteLine(ReceivedMessage);
            };


            //6、消费消息
            channel.BasicConsume(queue: "mymsg",autoAck:true, consumer: consumer);

            Console.ReadLine();
        }

        private static void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            
        }
    }
}
