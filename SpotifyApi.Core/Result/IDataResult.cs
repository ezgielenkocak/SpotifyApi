﻿namespace SpotifyApi.Core.Result
{
    public interface IDataResult<out T> : IResult
    {
        T Data { get; }
    }
}
