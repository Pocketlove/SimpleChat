using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;

namespace SimpleChat
{
    class Program
    {
        public static int socket_address = 80;
        public static Encoding en_US = Encoding.GetEncoding(1251);
        public static bool should_close = false;
        public static string socket_address_string = "";
        public static bool doexit = false;
        public static bool total_termination = false;
        public static string nickname_current = "";
        public static bool can_restart_connection = false;
        public static string msg_for_disconnect = "";
        public static bool socket_checked = false;
        public static int music_state = 0;
        public static string ip_adress_string = "";
        public static string ip_address_string;

        public static bool false_address = false;


        public static int symbamount = 0;
        public static int second_symbamount = 0;
        public static int save_second_symbamount = 0;
        public static int third_symbamount = 0;


        public static Socket something_more_server_cl = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        public static void PlayMusic()
        {
            while (true)
            {
                if (music_state == 1)
                {
                    Console.Beep(300, 100);
                    Thread.Sleep(50);
                    Console.Beep(400, 100);
                    Thread.Sleep(50);
                    Console.Beep(500, 200);
                    Thread.Sleep(50);
                    music_state = 0;
                }
                if (music_state == 2)
                {
                    Console.Beep(500, 200);
                    Thread.Sleep(50);
                    Console.Beep(400, 100);
                    Thread.Sleep(50);
                    Console.Beep(300, 100);
                    Thread.Sleep(50);
                    music_state = 0;
                }
            }
        }

        public static void Client()
        {
            Console.WriteLine("Client Threading Started...");

           

            PointOfEstablishment:
            string msg;
            string msg_raw;
            char[] msg_char;
            DateTime timestamp;
            
            string socket_address_string = "";
            int socket_adress = 80;
            string ip_address = "";
            string doconnect;
            string nickname;

            IdiotProofSpecific:
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nTo establish connection, type [/establish]\n");
            Console.ResetColor();
            IdiotProof:
            doconnect = Console.ReadLine();
            if(doconnect != "/establish")
            {
                if (doconnect == "/exit")
                {
                    doexit = true;
                    System.Environment.Exit(0);
                }
                else if (doconnect == "/cls")
                {
                    Console.Clear();
                    IPScan();
                    goto IdiotProofSpecific;
                }
                else
                {
                    goto IdiotProof;
                }
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nPlease, type in receiving computer's address: \n");
            Console.ResetColor();
            IdiotProof2:
            ip_address = Console.ReadLine();
            //Address Check
            try
            {
                IPAddress.Parse(ip_address);
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Incorrect address. Please, try again\n");
                Console.ResetColor();
                goto IdiotProof2;
            }
            IPAddress ip = IPAddress.Parse(ip_address);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nPlease, input receiving computer's socket number: \n");
            Console.ResetColor();
            socket_address_string = Console.ReadLine();
            //Socket Check
            try
            {
                socket_adress = Convert.ToInt32(socket_address_string);
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Incorrect socket address. Please, input receiving computer's address again: \n");
                Console.ResetColor();
                goto IdiotProof2;
            }
            IPEndPoint something_else = new IPEndPoint(ip, socket_adress);
            
            //Connection Check
            try
            {
                something_more_server_cl.Connect(something_else);
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nCould not connect. Please try again later.\n");
                Console.ResetColor();
                goto PointOfEstablishment;
            }
            //Nickname Check
            NicknamePoint:
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nPlease, type in your nickname ([Space] and [:] symbols are not allowed): \n");
            Console.ResetColor();
            nickname = Console.ReadLine();

            for(int i = 0; i < nickname.Length; i++)
            {
                if(nickname[i] == ':' || nickname[i] == ' ')
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Unacceptable symbol!\n");
                    Console.ResetColor();
                    goto NicknamePoint;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Connected!");
                    Console.ResetColor();
                    Thread.Sleep(2000);
                    break;
                }
            }

            StringBuilder stringbuilder = new StringBuilder();
            bool first_type = true;
            while (doexit == false)
            {
                string timestamp_string;
                string timestamp_string_final = "";
                char[] timestamp_char;
                string update_msg_raw = "";
                int number_of_symbols = 0;

                msg_raw = "";
                ReType:
                while (true)
                {
                    if (first_type == true)
                    {
                        Console.Clear();
                        first_type = false;
                        IPScan();
                        Console.ForegroundColor = ConsoleColor.Green;
                        if (nickname_current == "")
                        {
                            Console.WriteLine("Chat started...");
                        }
                        else
                        {
                            Console.WriteLine("Currently in chat with " + nickname_current + " (" + something_else.ToString() + ")");
                        }
                        Console.ResetColor();
                    }
                    var pressedkey = System.Console.ReadKey(false);
                    number_of_symbols++;
                    if (pressedkey.Key == ConsoleKey.Enter)
                    {
                        number_of_symbols = 0;
                        break;

                    }
                    if(pressedkey.Key == ConsoleKey.Backspace)
                    {
                        number_of_symbols = number_of_symbols - 2;
                        Console.Write(" \b \b");
                        if (number_of_symbols >= 0)
                        {
                            msg_raw = msg_raw.Substring(0, number_of_symbols);
                        }
                    }
                    else
                    {
                        msg_raw = msg_raw + pressedkey.KeyChar;
                    }
                    

                }
                if (msg_raw != "")
                {
                    timestamp = DateTime.Now;
                    timestamp_string = timestamp.ToString();
                    timestamp_char = timestamp_string.ToCharArray();
                    bool startprinting = false;
                    for (int i = 0; i < timestamp_char.Length; i++)
                    {
                        if (startprinting == true)
                        {
                            timestamp_string_final = timestamp_string_final + timestamp_char[i];
                        }
                        if (timestamp_char[i] == ' ')
                        {
                            startprinting = true;
                        }
                    }
                    msg = "[" + timestamp_string_final + "] " + nickname + ": " + msg_raw;
                    Console.WriteLine(msg);

                    if (msg == "[" + timestamp_string_final + "] " + nickname + ": " + "/disconnect")
                    {
                        nickname_current = "";
                        can_restart_connection = true;
                        doexit = true;
                        total_termination = true;



                        msg = "/disconnect";

                        byte[] bytes = en_US.GetBytes(msg);
                        something_more_server_cl.Send(bytes);
                    }
                    
                    if (msg == "[" + timestamp_string_final + "] " + nickname + ": " + "/cls")
                    {
                        Console.Clear();
                        IPScan();
                        Console.ForegroundColor = ConsoleColor.Green;
                        if (nickname_current == "")
                        {
                            Console.WriteLine("Chat started...");
                        }
                        else
                        {
                            Console.WriteLine("Currently in chat with " + nickname_current + " (" + something_else.ToString() + ")");
                        }
                        Console.ResetColor();
                    }
                    else if (msg == "[" + timestamp_string_final + "] " + nickname + ": " + "/exit")
                    {
                        System.Environment.Exit(0);
                        something_more_server_cl.Disconnect(true);
                        doexit = true;
                        Console.WriteLine("Not Terminated");
                    }
                    else
                    {
                        
                        byte[] bytes = en_US.GetBytes(msg);
                        music_state = 1;
                        something_more_server_cl.Send(bytes);
                    }
                }
                else
                {
                    goto ReType;
                }
            }
        }


        public static string IPScan()
        {
            string localmaschinename = Dns.GetHostName();

            if (false_address == false)
            {
                IPHostEntry localmaschineip = Dns.GetHostByName(localmaschinename);
                IPAddress[] ip_adress_mass = localmaschineip.AddressList;
                for (int i = 0; i < ip_adress_mass.Length; i++)
                {
                    ip_adress_string = ip_adress_string + ip_adress_mass[i].ToString();
                }
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Current Computer Name: " + localmaschinename + "\n" + "Current Computer IP: " + ip_adress_string);
            if(socket_checked == true)
            {
                Console.WriteLine("Current Computer Socket: " + socket_address_string + "\n");
            }
            Console.ResetColor();
            return (ip_adress_string);
        }
        static void Main(string[] args)
        {
        Point:
            Thread Music_Thread = new Thread(PlayMusic);
            Music_Thread.Start();
            can_restart_connection = false;
            total_termination = false;
            socket_checked = false;
            doexit = false;
            socket_address = 80;
            socket_address_string = "";
            ip_address_string = IPScan();
            
            try
            {
                IPAddress.Parse(ip_address_string);
            }
            catch(System.FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Could not resolve an IP address for local machine. Please input IP Address manually:");
                ip_adress_string = Console.ReadLine();
                Console.ResetColor();
                Console.Clear();
                false_address = true;
                goto Point;
            }

            //Socket Creation
            IPAddress ip = IPAddress.Parse(ip_address_string);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nInput your socket number: ");
            Console.ResetColor();
            SocketEntry:
            socket_address_string = Console.ReadLine();
            
            try
            {
                socket_address = Convert.ToInt32(socket_address_string);
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nIncorrect socket. Please, try again\n");
                Console.ResetColor();
                goto SocketEntry;
            }
            socket_checked = true;
            Console.Clear();
            IPScan();
            IPEndPoint something_else = new IPEndPoint(ip, socket_address);
            Socket something_more_server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            doexit = false;
            Point2:
            try
            {
                something_more_server.Bind(something_else);
            }

            catch (System.Net.Sockets.SocketException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Socket not established. Retring...");
                Console.ResetColor();
                Thread.Sleep(2000);
                Console.Clear();
                goto Point;
            }
            //something_more_server.Bind(something_else);
            
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Socket established. Awaiting connection on socket: " + something_else.ToString() + "\n");
            Console.ResetColor();
            Thread Thread1 = new Thread(new ThreadStart(Client));
            Thread1.Start();
            something_more_server.Listen(20000);

            Socket something_more_client = something_more_server.Accept();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nForeign connection established. Chat started.\n");
            Console.ResetColor();


            while (doexit == false && can_restart_connection != true)
            {
                if (can_restart_connection == true)
                {
                    goto Point;
                    break;
                }
                Point3:
                byte[] bytes = new byte[256];

                StringBuilder stringbuilder = new StringBuilder();
                if (doexit == false)
                {
                    try
                    {

                        something_more_client.Receive(bytes);
                    }
                    catch (System.Net.Sockets.SocketException)
                    {
                        total_termination = true;
                    }
                }
                else
                {
                    total_termination = true;
                }
                stringbuilder.Append(en_US.GetString(bytes));
                string msg = stringbuilder.ToString();
                string msg_check = "";
                string other_nickname = "";
                char[] msgs = msg.ToCharArray();
                for (int i = 0; i < 256; i++)
                {
                    if (msgs[i] == ' ' && save_second_symbamount == 0)
                    {
                        save_second_symbamount = 1;
                        third_symbamount = i;
                    }
                    if (msgs[i] == ' ' && save_second_symbamount == 2)
                    {
                        second_symbamount = i;
                        save_second_symbamount = 3;
                    }
                    if (msgs[i] == '\0' && save_second_symbamount == 3)
                    {
                        symbamount = i;
                        break;
                    }
                    if(save_second_symbamount == 1)
                    {
                        save_second_symbamount = 2;
                    }
                }
                try
                {
                    msg_check = msg.Substring(second_symbamount + 1, symbamount - second_symbamount - 1);
                    nickname_current = msg.Substring(third_symbamount + 1, second_symbamount - third_symbamount - 2);
                    msg.Substring(0, symbamount);
                }
                catch
                {
                    total_termination = true;
                }
                if (total_termination != true)
                {
                    if (msg_check != "/exit" || msg_check != "/disconnect")
                    {
                        music_state = 2;
                        Console.WriteLine(msg.Substring(0, symbamount));
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nUser has terminated connection...");
                        bytes = en_US.GetBytes("/disconnect");
                        try
                        {
                            something_more_server_cl.Send(bytes);
                        }
                        catch
                        {
                        }
                        something_more_server_cl.Close();
                        something_more_client.Close();
                        something_more_server.Close();
                        Thread.Sleep(5000);
                        Console.ResetColor();
                        Console.Clear();
                        should_close = true;
                        Thread1.Abort();
                        goto Point;
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nUser has terminated connection...");
                    bytes = en_US.GetBytes("/disconnect");
                    try
                    {
                        something_more_server_cl.Send(bytes);
                    }
                    catch
                    {
                    }
                    something_more_server_cl.Close();
                    something_more_client.Close();
                    something_more_server.Close();
                    Thread.Sleep(5000);
                    Console.ResetColor();
                    Console.Clear();
                    should_close = true;
                    Thread1.Abort();
                    goto Point;
                }
            }

        }
        
    }

}