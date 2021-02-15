using Core.Base.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Core.Models;
using Microsoft.Extensions.Logging;
using Core.Data;

namespace Core.Services.Data
{
    //TODO: for refactoring.
    public class NumberCounterService: INumberCounterService
    {
        private readonly object _locker = new object();
        private readonly CoreDbContext _context;
        private readonly ILogger<NumberCounterService> _logger;
        private CounterConfig _options;

        public NumberCounterService(CoreDbContext context, ILogger<NumberCounterService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void ConfigureCounter(Action<CounterConfig> options)
        {
            _options = new CounterConfig();
            options.Invoke(_options);
        }

        public string GetNumberCounter()
        {
            ValidateOptions();
            lock (_locker)
            {
                return GenerateNumber();
            }
        }

        private void ValidateOptions()
        {
            if (_options is null)
            {
                _logger.LogError($"Required options are missing in {nameof(NumberCounterService)}");
            }

            //TODO: add validation to CounterConfig
        }

        private NumberCounter GetLastNumberCounter()
        {
            return _context.Set<NumberCounter>()
                .AsNoTracking()
                .FirstOrDefault(x => x.EntityName == _options.EntityName &&
                    x.EntityId == _options.EntityId &&
                    x.Caption == _options.CounterName &&
                    x.RecordState != RecordState.Deleted);
        }

        private void SaveNumberCounter(NumberCounter res)
        {
            if (res.Id != Guid.Empty)
            {
                _context.Update(res);
            }
            else
            {
                _context.Attach(res);
            }

            //TODO: no need to save before transaction is not ended.
            //_context.SaveChanges();
        }

        private string GenerateNumber()
        {
            var res = GetLastNumberCounter();

            if (res == null)
            {
                res = new NumberCounter()
                {
                    Caption = _options.CounterName,
                    EntityName = _options.EntityName,
                    EntityId = _options.EntityId,
                    CounterType = _options.CounterType,
                    Pattern = _options.Pattern,
                    Value = "0"
                };
            }

            if (TryGetCounterNumber(res.Value, out var currentNumber))
            {
                currentNumber += 1;
                res.Value = currentNumber.ToString();
                SaveNumberCounter(res);
                return res.Value;
            }

            return string.Empty;
        }

        private bool TryGetCounterNumber(string value, out int number)
        {
            number = 0;//????

            try
            {
                number = Convert.ToInt32(value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error when trying to get number for entity - {0} and counter name - {1} (Value = {2})",
                    _options.EntityName, _options.CounterName, value);
                return false;
            }

            return true;
        }
    }
}
