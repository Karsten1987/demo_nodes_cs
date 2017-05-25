using System;
using rclcs;
namespace listener_cs
{
	/**
	 * This example shows how to create a subscription
	 */
	public class Listener
	{
		public static void Main (string[] args)
		{
			
			//Create instance of RCL class which handles functions from rcl.h
			//RCL implements IDisposable so the using statement makes sure rcl_shutdown will be called after usages
			using (RCL rcl = new RCL ()) {
				//Initialise RCL with default allocator
				rcl.Init (args);

				//Create an executor that will task our node
				Executor demoExecutor = new SingleThreadedExecutor();
				//Let the executor task all nodes with the given timespan
				demoExecutor.Spin (new TimeSpan (0, 0, 0, 0, 10));
				//Create a Node
				using (Node testNode = new Node ("listener_cs")) {
					//Add node to executor
					demoExecutor.AddNode(testNode);

					//Create subscription on TestTopic
					using (Subscription<std_msgs.msg.Int32> testSub = testNode.CreateSubscription<std_msgs.msg.Int32> ("chatter")) {
						//Register on MessageRecieved event
						testSub.MessageRecieved += (object sender, MessageRecievedEventArgs<std_msgs.msg.Int32> e) => 
						{
                            Console.WriteLine("I heard: " + e.Message.data);
							//Simply print all message items
							foreach (var item in e.Message.GetType().GetFields()) {
								Console.Write (item.Name + "      :" + item.GetValue (e.Message));
								Console.WriteLine ();
							}
						};
						//Call readkey so the program won't close instantly
						Console.ReadKey ();
					}
				}
				//Remember to stop the executor - you could start it again afterwards
				demoExecutor.Cancel ();

			}
			//rcl_shutdown gets called automatically
		}
	}
}

