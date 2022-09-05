using System;
using System.Collections;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Threading;

namespace Medical.Consumer
{
    internal class Program
    {
        /// <summary>
        /// 发布订阅模式   观察者模式    轮询
        /// </summary>
        /// <param name="args"></param>
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

            //声明交换器——广播
            channel.ExchangeDeclare(exchange: "amqp.fanout.1911", ExchangeType.Fanout);

            //4、通过通道声明一个队列
            var queueName = channel.QueueDeclare().QueueName;

            //绑定队列到交换器
            channel.QueueBind(queue: queueName, exchange: "amqp.fanout.1911", routingKey: "");

            //4、创建消费者
            var consumer = new EventingBasicConsumer(channel);

            //Console.WriteLine("接收参数：" + args[0]);


            //网络吞吐量
            channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            //5、接收消息
            consumer.Received += (sender, ea) => {
                //接收消息
                string ReceivedMessage = string.Empty;

                //接收到的数据
                var ReceivedData = ea.Body.ToArray();

                //字节数组转字符串
                ReceivedMessage = Encoding.UTF8.GetString(ReceivedData);

                //处理消息
                //codeing.......
                //线程睡眠10秒   耗时
                //Thread.Sleep(TimeSpan.FromSeconds(Convert.ToInt16(args[0])));

                Console.WriteLine("+++++++++++++");
                Console.WriteLine("接收到的消息");
                Console.WriteLine("+++++++++++++");
                Console.WriteLine(ReceivedMessage);

                //手动确认
                channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
            };

            //6、消费消息
            channel.BasicConsume(queue: queueName, autoAck:false, consumer: consumer);

            Console.ReadLine();
        }

        private static void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            
        }
    }
}
