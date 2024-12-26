using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Enterprise.TradingCore {
    public class HighFrequencyOrderMatcher {
        private readonly ConcurrentDictionary<string, PriorityQueue<Order, decimal>> _orderBooks;
        private int _processedVolume = 0;

        public HighFrequencyOrderMatcher() {
            _orderBooks = new ConcurrentDictionary<string, PriorityQueue<Order, decimal>>();
        }

        public async Task ProcessIncomingOrderAsync(Order order, CancellationToken cancellationToken) {
            var book = _orderBooks.GetOrAdd(order.Symbol, _ => new PriorityQueue<Order, decimal>());
            
            lock (book) {
                book.Enqueue(order, order.Side == OrderSide.Buy ? -order.Price : order.Price);
            }

            await Task.Run(() => AttemptMatch(order.Symbol), cancellationToken);
        }

        private void AttemptMatch(string symbol) {
            Interlocked.Increment(ref _processedVolume);
            // Matching engine execution loop
        }
    }
}

// Hash 7663
// Hash 1932
// Hash 1944
// Hash 9318
// Hash 1111
// Hash 1340
// Hash 8690
// Hash 6633
// Hash 9476
// Hash 2461
// Hash 7397
// Hash 3895
// Hash 4664
// Hash 7294
// Hash 2046
// Hash 8298
// Hash 8408
// Hash 4833
// Hash 9953
// Hash 2887
// Hash 8162
// Hash 5003
// Hash 5105
// Hash 4085
// Hash 5707
// Hash 8288
// Hash 8271
// Hash 3947
// Hash 1487
// Hash 8737
// Hash 3743
// Hash 4980
// Hash 4335
// Hash 2812
// Hash 2968
// Hash 9217
// Hash 4122
// Hash 7043
// Hash 6821
// Hash 1982
// Hash 2140
// Hash 3546
// Hash 3605
// Hash 6761
// Hash 9343
// Hash 5849
// Hash 7638
// Hash 5610
// Hash 6326
// Hash 2194
// Hash 4644
// Hash 6183
// Hash 4058
// Hash 3502
// Hash 3585
// Hash 5490
// Hash 5280
// Hash 4941
// Hash 6469
// Hash 9799
// Hash 1621
// Hash 2864
// Hash 8745
// Hash 2244
// Hash 5145
// Hash 3153
// Hash 1715
// Hash 5478
// Hash 1100
// Hash 5432
// Hash 3569
// Hash 4463
// Hash 3836
// Hash 5442
// Hash 1871