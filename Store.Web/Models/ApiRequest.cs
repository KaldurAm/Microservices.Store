﻿using Store.Web.Options;

namespace Store.Web.Models;

public class ApiRequest
{
    public ApiType ApiType { get; set; } = ApiType.GET;
    public string Url { get; set; }
    public string AccessToken { get; set; }
    public object Data { get; set; }
}