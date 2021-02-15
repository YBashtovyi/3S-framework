using System;
using Core.Data;

namespace Core.Services.Data
{
    public interface INumberCounterService
    {
        void ConfigureCounter(Action<CounterConfig> options);
        string GetNumberCounter();
    }
}
