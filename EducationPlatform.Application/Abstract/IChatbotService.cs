﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EducationPlatform.Application.Abstract
{
    public interface IChatbotService
    {
        Task<string> GetChatbotResponseAsync(string message);
    }
}
