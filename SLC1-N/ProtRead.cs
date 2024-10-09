using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SLC1_N
{
   

        public class SerialPortReader
        {
         public SerialPort _serialPort;
            private Thread _readThread;
            private bool _keepReading;
            private StringBuilder _receivedData;
            private int _timeoutMilliseconds;
            private Timer _timer;
            public string Serial_write;


            public event Action<string> DataReceived;
      
            public SerialPortReader(string portName, int baudRate, int timeoutMilliseconds = 1000)
            {

            _serialPort = new SerialPort(portName, baudRate);
            _receivedData = new StringBuilder();
                _timeoutMilliseconds = timeoutMilliseconds;
                _timer = new Timer(OnTimeout, null, Timeout.Infinite, Timeout.Infinite);
            }
        public void Write(string data)
        {

            Serial_write= data;
            //if(_serialPort.IsOpen)
            //_serialPort.WriteLine(data);
        }
        public void Start()
            {
                if (!_serialPort.IsOpen)
                {
                    _serialPort.Open();
                }
                _keepReading = true;
                _readThread = new Thread(Read);
                _readThread.Start();
            }

            public void Stop()
            {
                _keepReading = false;
                if (_serialPort.IsOpen)
                {
                    _serialPort.Close();
                }
                _readThread.Join();
                _timer.Dispose();
            }

            private void OnTimeout(object state)
            {
                if (_receivedData.Length > 0)
                {
                    DataReceived?.Invoke(_receivedData.ToString());
                    _receivedData.Clear();
                if (_serialPort.IsOpen)
                {
                    _serialPort.DiscardInBuffer();
                    _serialPort.DiscardOutBuffer();
                }
            }
            }

            private void Read()
            {
                _timer.Change(_timeoutMilliseconds, Timeout.Infinite);

                while (_keepReading)
                {
                    try
                    {
                      
                        string data = _serialPort.ReadExisting();
                        if (!string.IsNullOrEmpty(data))
                        {
                            _receivedData.Append(data);
                             
                            _timer.Change(_timeoutMilliseconds, Timeout.Infinite); // Reset the timer
                        }
                    }
                    catch (TimeoutException)
                    {
                        // Handle the timeout exception if needed

                    }

                try
                {
                    if(_serialPort.IsOpen&& Serial_write!="")
                   _serialPort.WriteLine(Serial_write);
                    Serial_write = "";
                }
                catch 
                { 
                
                }


                }
            }
        }

    }

