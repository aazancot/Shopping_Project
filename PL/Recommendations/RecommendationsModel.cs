using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;
using Accord.MachineLearning.Rules;
using BE;

namespace PL.Recommendations
{
    public class RecommendationsModel
    {
        IBL BL;

        public RecommendationsModel()
        {
            BL = new BLImp();
        }

        public List<ProductOrder> GetAllProductOrders(Func<BE.ProductOrder, bool> predicate = null)
        {
            return BL.GetAllProductOrders(predicate);
        }
        public AssociationRule<Product>[] GetAssociationRules()
        {
            return BL.GetAssociationRules();
        }
        public void CreatePDF(List<object[]> items)
        {
            BL.CreatePDF(items);
        }


    }
    public class RecommendatedItems
    {
        public int BarCode { get; set; }
        public int TotalCount { get; set; }
        public int StoreId { get; set; }
        public string CheapUnitPrice { get; set; }

        public bool IsChecked { get; set; }

    }

    public class AssociationRules :IEquatable<AssociationRules>
    {
        public string X { get; set; }
        public string Y { get; set; }
        public Double Confidence { get; set; }

        public bool Equals(AssociationRules other)
        {

            //Check whether the compared object is null.
            if (Object.ReferenceEquals(other, null)) return false;

            //Check whether the compared object references the same data.
            if (Object.ReferenceEquals(this, other)) return true;

            //Check whether the products' properties are equal.
            return X.Equals(other.X) && Y.Equals(other.Y) && Confidence.Equals(other.Confidence);
        }

        // If Equals() returns true for a pair of objects
    // then GetHashCode() must return the same value for these objects.
    
        public override int GetHashCode()
        {

            
            int hashX = X == null ? 0 : X.GetHashCode();

          
            int hashY = Y.GetHashCode();

            
            int hashConfidence = Confidence.GetHashCode();

            

            return hashX ^ hashY ^ hashConfidence ;
        }

    }
}

