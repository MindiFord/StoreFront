using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; // Added for access to DataAnnotations

namespace StoreFront.DATA.EF/*.StoreFrontMetadata*/ // ** a
{

    //Short process for each pair of metadata class and EF class:
    //  a) Ensure that the namespaces for files match (match the.cs namespace)
    //  b) Create the metadata class (empty)
    //  c) Create the partial class with the same EXACT name as the EF class
    //  d) Apply the metadata class to the partial class
    //  e) Use the EF class to copy in the properties for the metadata class
    //  f) Apply all necessary metadata attributes

    #region Category Metadata


    public class CategoryMetadata // ** b
    {

        // ** e -- fields/props copied from Category.cs (StoreFront.edmx -> StoreFront.tt -> Category.cs)

        // Database will help handle the validation aspects of the ID field.
        //public int CategoryID { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = " * Cannot exceed 30 characters")]
        [Display(Name = "Category")]
        public string CategoryName { get; set; }
    }

    [MetadataType(typeof(CategoryMetadata))] // ** d
    public partial class Category // ** c
    {

    }


    #endregion

    #region Department Metadata

    public class DepartmentMetadata
    {
        //public int DeptID { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = " * Cannot exceed characters")]
        [Display(Name = "Department")]
        public string Department1 { get; set; }
    }

    [MetadataType(typeof(DepartmentMetadata))]
    public partial class Department
    {

    }

    #endregion

    #region EmployeeMetadata

    public class EmployeeMetadata
    {

        //public int EmployeeID { get; set; }

        [Required]
        public int DeptID { get; set; }

        //public Nullable<int> DirectReportID { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = " * Cannot exceed 20 characters")]
        [Display(Name = "First Name")]
        public string FName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = " * Cannot exceed 20 characters")]
        [Display(Name = "Last Name")]
        public string LName { get; set; }

        [DisplayFormat(NullDisplayText = " n/a ")]
        [StringLength(50, ErrorMessage = " * Cannot exceed 50 characters")]
        public string Address { get; set; }

        [DisplayFormat(NullDisplayText = " n/a ")]
        [StringLength(50, ErrorMessage = " * Cannot exceed 50 characters")]
        public string City { get; set; }

        [DisplayFormat(NullDisplayText = " n/a ")]
        [StringLength(2, ErrorMessage = " * Cannot exceed 2 characters")]
        public string State { get; set; }

        [DisplayFormat(NullDisplayText = " n/a ")]
        [StringLength(6, ErrorMessage = " * Cannot exceed 6 characters")]
        public string Zip { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

    }

    [MetadataType(typeof(EmployeeMetadata))]
    public partial class Employee
    {
        // Custom, read only property for FullName
        [Display(Name = "Employee")]
        public string FullName => FName + ' ' + LName;

        
        public string CityStateZip => City + ", " + State + " " + Zip;
        
    }

    #endregion

    #region PricePer Metadata

    public class PricePerMetadata
    {
        //public int PriceID { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = " * Cannot exceed 20 characters")]

        public string Description { get; set; }
    }

    [MetadataType(typeof(PricePerMetadata))]
    public partial class PricePer
    {

    }

    #endregion

    #region Product Metadata

    public class ProductMetadata
    {
        //public int ProductID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = " * Cannot exceed 50 characters")]
        [Display(Name = "Product")]
        public string ProductName { get; set; }

        [Required]
        public int CategoryID { get; set; }

        [DisplayFormat(NullDisplayText = " n/a ")]
        public string Image { get; set; }

        [DisplayFormat(NullDisplayText = " n/a ")]
        [UIHint("MultilineText")]
        [Display(Name = "Description")]
        public string ProductDesc { get; set; }

        [Required]
        [Display(Name = "Price")]
        [Range(0, double.MaxValue, ErrorMessage = " * Must be valid numeric value")]
        [DisplayFormat(DataFormatString = "{0:c}, ApplyFormatInEditMode = true")]
        public decimal UnitPrice { get; set; }

        [Required]
        public int PriceID { get; set; }

        [Required]
        public int StatusID { get; set; }

    }

    [MetadataType(typeof(ProductMetadata))]
    public partial class Metadata
    {

    }

    #endregion

    #region Status Metadata

    public class StatusMetadata
    {
        //public int StatusID { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = " * Cannot exceed 30 characters")]
        public string Description { get; set; }
    }

    [MetadataType(typeof(StatusMetadata))]
    public partial class Status
    {

    }

    #endregion

}
