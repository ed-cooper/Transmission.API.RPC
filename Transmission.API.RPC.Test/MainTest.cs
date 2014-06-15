﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Transsmission.API.RPC;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Transsmission.API.RPC.Entity;

namespace Transmission.API.RPC.Test
{
    [TestClass]
    public class MainTest
    {
        const string SESSION_ID = "eyCd0F1Yxc1ljG4GbROe3542HH6PneD3NjcqmXtnKT6E5Y6b";
        const string HOST = "http://192.168.1.50:9091/transmission/rpc";

        public Client client = new Client();

        [TestInitialize]
        public void Initialize()
        {
            client.Host = HOST;
            client.SessionID = SESSION_ID;
        }

        [TestMethod]
        public void GetSession()
        {
            var sessionInfo = client.GetSession();
        }

        [TestMethod]
        public void GetActiveTorrents()
        {         
            var activeTorrents = client.TorrentsGetActive(client.AllTorrentsFields);
        }

        [TestMethod]
        public void GetAllTorrents()
        {
            var allTorrents = client.TorrentsGetAll(client.AllTorrentsFields);
        }

        [TestMethod]
        public void CheckPort()
        {
            client.CheckPort();
        }

        [TestMethod]
        public void AddTorrent()
        {
            var filePath = "D:\\test.torrent";
            var fstream = File.Open(filePath, FileMode.Open);
            var base64 = ConvertToBase64(fstream);
            TransmissionNewTorrent newTorrent = new TransmissionNewTorrent
            {
                Metainfo = base64,
                Paused = false,
                //TODO: Add and check other arguments
                //<...>
            };

            var result = client.AddTorrent(newTorrent);
        }

        [TestMethod]
        public void RemoveTorrent()
        {
            var ids = new int[]{40};

            client.TorrentsRemove(ids);
        }

        public string ConvertToBase64(Stream stream)
        {
            Byte[] inArray = new Byte[(int)stream.Length];
            stream.Read(inArray, 0, (int)stream.Length);
            return Convert.ToBase64String(inArray, 0, inArray.Length);
        }
    }
}
