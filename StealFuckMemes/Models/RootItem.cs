﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StealFuckMemes.Models
{
    public class RootItem
    {
        [JsonPropertyName("data")]
        public List<MemeTemplate> Data { get; set; }
    }
}
