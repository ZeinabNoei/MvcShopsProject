﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System.ComponentModel.DataAnnotations;
namespace DataLayer
{
    internal class Product_FeaturesMetaData
    {
        public int ProductFeatureID { get; set; }

        [Display(Name = "Product")]
        [Required(ErrorMessage = "Please Enter Product:")]
        public int ProductID { get; set; }

        [Display(Name = "Feature")]
        [Required(ErrorMessage = "Please Enter Feature :")]
        public int FeatureID { get; set; }

        [Display(Name = "Quantity")]
        [Required(ErrorMessage = "Please Enter Quantity :")]
        public string ProductFeaturesValue { get; set; }
    }
}