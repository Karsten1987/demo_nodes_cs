﻿using System;
using rclcs;

namespace talker_cs
{
	/**
	 * This example shows how to create a publisher
	 */
	public class Talker
	{
		public static void Main (string[] args)
		{
			
			//Create instance of RCL class which handles functions from rcl.h
			//RCL implements IDisposable so the using statement makes sure rcl_shutdown will be called after usages
			using (RCL rcl = new RCL ()) {
				//Initialise RCL with default allocator
				rcl.Init (args);

				//Create an executor that will task our node
				Executor demoExecutor = new SingleThreadedExecutor ();
				//Let the executor task all nodes with the given timespan
				demoExecutor.Spin (new TimeSpan (0, 0, 0, 0, 10));
				//Create a Node
				using (Node testNode = new Node ("talker_cs")) {
					//Add node to executor
					demoExecutor.AddNode (testNode);

					//Now we're creating a publisher with the Dummy message
					//TODO show alternative to Node.CreatePublisher<T>
					//using (Publisher<test_msgs.msg.Dummy> testPublisher = testNode.CreatePublisher<test_msgs.msg.Dummy> ("TestTopic")) {
                        using (Publisher<std_msgs.msg.Int32> testPublisher = testNode.CreatePublisher<std_msgs.msg.Int32>("chatter"))
                        {

                            //Create a message 
                            using (std_msgs.msg.Int32 testMsg = new std_msgs.msg.Int32 ()) {
                            testMsg.data = 999666;

                            for (int i = 0; i < 1; ++i)
                            {
                                //And now publish the message
                                Console.WriteLine("Publishing " + testMsg.data);
                                testPublisher.Publish(testMsg);
                                System.Threading.Thread.Sleep(1000);
                            }
							//Free unmanaged memory

                            
                            }

                        }

                    }
				//Remember to stop the executor - you could start it again afterwards
				demoExecutor.Cancel ();

			}
			//rcl_shutdown gets called automatically
		}
	}
}

