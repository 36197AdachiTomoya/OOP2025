using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesCalculator{
    //売り上げクラス
    public class Sale{
        //店舗名
        public required string ShopName { get; set; } = string.Empty;
        //商品カテゴリ
        public required string ProductCategory { get; set; } = string.Empty;
        //売上高
        public required int Amount { get; set; }
    }
}
