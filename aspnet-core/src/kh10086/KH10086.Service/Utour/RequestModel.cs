﻿using System;
namespace KH10086.Service.Utour
{
    public class RequestModel
    {
        public string visitCode { get; set; }
        public string key { get; set; }
        public string date { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public object param { get; set; }
    }
}