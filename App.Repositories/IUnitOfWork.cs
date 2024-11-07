namespace App.Repositories;
public interface IUnitOfWork
{
	Task<int> SaveChangesAsync();
}
#region Unif Of Work
/*
Unif Of Work pattern neden kullanılır?
ornegin StockRepository, OrderRepository, PaymentRepository'miz olsun, biz bu repolarimizda savechanges yapmayız cunku;
bir sipariş gerceklestiginde stockRe.Update(), paymentR.Create(), OrderR.Create() metotlari calisirken bazıları basarili bir sekilde olusurken
bazılarında hata olursa ve biz her birinde ayrı ayrı savechanges dersek veri butunlugu bozulur kimi database'e kaydolurken hatalı islem kaydolmaz
o yuzden 3u de basarili bir sekilde gerceklesirse o zaman kaydedilmesi istenir o yuzden bu islemi (db'ye kaydetme) UoW yapabilir.
 */
#endregion