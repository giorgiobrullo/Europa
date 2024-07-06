using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Other
{
    public class LiveSplitIntegration : MonoBehaviour
    {
        private int _checkpointCounter;
        private const string LiveSplitHost = "localhost";
        private const int LiveSplitPort = 16834;
        private Queue<string> _commandQueue = new();
        private bool _isProcessingQueue;
        private TcpClient _client;
        private NetworkStream _stream;

        // Singleton pattern
        public static LiveSplitIntegration Instance { get; private set; }

        
        public void ResetCheckpointCounter()
        {
            _checkpointCounter = 0;
        }
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                ConnectToLiveSplitServer();
                DontDestroyOnLoad(gameObject);
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        private void OnDestroy()
        {
            DisconnectFromLiveSplitServer();
        }

        public void EndRun()
        {
            EnqueueCommand("pause");
        }
        
        public void CheckpointReached(int id)
        {
            if (id <= _checkpointCounter) return;
            _checkpointCounter++;
            EnqueueCommand("split");
        }

        public void PauseRun()
        {
            EnqueueCommand("pause");
        }

        public void ResumeRun()
        {
            EnqueueCommand("resume");
        }

        public void SendStartGameEvent()
        {
            EnqueueCommand("starttimer");
        }

        public void SendSceneChangeEvent()
        {
            ResumeRun();
            EnqueueCommand("split");
            //EnqueueCommand($"setcurrentsplitname {sceneName}");
        }

        private void EnqueueCommand(string command)
        {
            Debug.Log("LiveSplit command enqueued: " + command);
            _commandQueue.Enqueue(command);
            if (!_isProcessingQueue)
            {
                Debug.Log("Starting to process LiveSplit command queue");
                StartCoroutine(ProcessCommandQueue());
            }
        }

        private IEnumerator ProcessCommandQueue()
        {
            _isProcessingQueue = true;
            while (_commandQueue.Count > 0)
            {
                string command = _commandQueue.Dequeue();
                Debug.Log("Processing LiveSplit command: " + command);
                yield return SendLiveSplitCommandAsync(command);
            }
            Debug.Log("Finished processing LiveSplit command queue");
            _isProcessingQueue = false;
        }

        private IEnumerator SendLiveSplitCommandAsync(string command)
        {
            Task task = Task.Run(() => SendLiveSplitCommand(command));
            yield return new WaitUntil(() => task.IsCompleted);

            if (task.Exception != null)
            {
                Debug.LogError($"Failed to send LiveSplit command: {task.Exception.GetBaseException().Message}");
            }
            else
            {
                Debug.Log($"LiveSplit command sent: {command}");
            }
        }

        private void ConnectToLiveSplitServer()
        {
            try
            {
                _client = new TcpClient(LiveSplitHost, LiveSplitPort);
                _stream = _client.GetStream();
                Debug.Log("Connected to LiveSplit server");
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to connect to LiveSplit server: {e.Message}");
            }
        }

        private void DisconnectFromLiveSplitServer()
        {
            if (_stream != null)
            {
                _stream.Close();
                _stream = null;
            }

            if (_client != null)
            {
                _client.Close();
                _client = null;
            }

            Debug.Log("Disconnected from LiveSplit server");
        }

        private void SendLiveSplitCommand(string command)
        {
            try
            {
                if (_stream != null)
                {
                    byte[] data = Encoding.ASCII.GetBytes($"{command}\r\n");
                    _stream.Write(data, 0, data.Length);
                    _stream.Flush();
                    Debug.Log($"LiveSplit command sent: {command}");
                }
                else
                {
                    Debug.LogError("Network stream is not available");
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Exception while sending LiveSplit command: {e.Message}");
                throw;
            }
        }
    }
}
