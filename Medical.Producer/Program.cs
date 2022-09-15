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

                //properties.Persistent = true;

                properties.DeliveryMode = 2;

                try
                {
                    channel.ConfirmSelect(); //将信道置为 publisher confirm 模式

                    // 之后正常发送消息
                    channel.BasicPublish(exchange: "amqp.topic.1911", routingKey: args[0], basicProperties: properties, messageBody);

                    if (!channel.WaitForConfirms())
                    {
                        Console.WriteLine("Send message failed");
                        // do something else...
                    }
                    else
                    {

                    }

                    //成功
                    channel.BasicAcks += (sender, args) => { 
                        
                    };

                    //不成功
                    channel.BasicNacks += (sender, args) => {

                    };
                }
                catch (Exception e)
                {
                    throw;
                }
            }

            /*// 开启事务
            channel.TxSelect();
            try
            {
                //发布消息
                channel.BasicPublish(exchange: "amqp.topic.1911", routingKey: args[0], basicProperties: properties, messageBody);
            }
            catch (Exception e)
            {
                channel.TxRollback();
                // 重发消息
            }
            // 提交事务
            channel.TxCommit();*/

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
