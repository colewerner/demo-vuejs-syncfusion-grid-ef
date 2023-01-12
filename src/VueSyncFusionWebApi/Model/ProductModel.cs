namespace VueSyncFusionWebApi.Model
{
    public class ProductModel
    {
        #region Instance Properties

        public virtual int CategoryId
        {
            get;
            set;
        }

        public virtual string CategoryName
        {
            get;
            set;
        }

        public virtual bool Discontinued
        {
            get;
            set;
        }

        public virtual int? ProductId
        {
            get;
            set;
        }

        public virtual string ProductName
        {
            get;
            set;
        }

        public virtual string QuantityPerUnit
        {
            get;
            set;
        }

        public virtual short? ReorderLevel
        {
            get;
            set;
        }

        public virtual int SupplierId
        {
            get;
            set;
        }

        public virtual string SupplierName
        {
            get;
            set;
        }

        public virtual decimal? UnitPrice
        {
            get;
            set;
        }

        public virtual short? UnitsInStock
        {
            get;
            set;
        }

        public virtual short? UnitsOnOrder
        {
            get;
            set;
        }

        #endregion
    }
}
