using Turnos_Backend.Models;

namespace Turnos_Backend.Services
{
    public class CounterService : ICounterService
    {
        private Dictionary<CustomerType, int> _customerTypeCounters;

        public CounterService()
        {
            _customerTypeCounters = new Dictionary<CustomerType, int>
            {
                { CustomerType.IOMA_PAMI, 0 },
                { CustomerType.Obra_Social, 0 },
                { CustomerType.Particular, 0 },
                { CustomerType.Perfumeria, 0 }
            };
        }
        
        public int GetNextCounter(int customerType)
        {
            //This is the explicit cast to the enum type
            var customerTypeId = (CustomerType)customerType;

            _customerTypeCounters[customerTypeId]++;
            CheckBelowHundred();

            return _customerTypeCounters[customerTypeId];
        }

        void CheckBelowHundred()
        {
            foreach(CustomerType type in Enum.GetValues(typeof(CustomerType)))
            {
                if(_customerTypeCounters[type] > 99)
                {
                    _customerTypeCounters[type] = 0;
                }
            }
        }
    }
}
