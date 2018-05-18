using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace cjcsessionapp.Models
{
    public class ListsHelperModel
    {
        public static IEnumerable<SelectListItem> DelegateType()
        {
            List<SelectListItem> delegatetypes = new List<SelectListItem>()
            {
                new SelectListItem() {Text = "Regular", Value = "Regular" },
                new SelectListItem() {Text = "Delegate At Large", Value = "Delegate At Large"},
                new SelectListItem() {Text = "Special Delegate", Value="Special Delegate", },
                new SelectListItem() {Text = "Guest", Value = "Gueset"}          
            };
            return delegatetypes;
        }

        public static IEnumerable<SelectListItem> GetNameTitles()
        {
            List<SelectListItem> titles = new List<SelectListItem>()
            {
                new SelectListItem() {Text = "Mr.", Value = "Mr." },
                new SelectListItem() {Text = "Mrs.", Value = "Mrs."},
                new SelectListItem() {Text = "Miss", Value="Miss", },
                new SelectListItem() {Text = "Ms.", Value = "Ms."},
                new SelectListItem() {Text = "Dr.", Value = "Dr."},
                new SelectListItem() {Text = "Pastor", Value = "Pastor"}
            };
            return titles;
        }

        public static IEnumerable<SelectListItem> GetGenderList()
        {
            List<SelectListItem> items = new List<SelectListItem>()
            {
                new SelectListItem() { Text = "Male", Value = "Male" },
                new SelectListItem() { Text = "Female", Value = "Female" }
            };
            return items;
        }

        public static IEnumerable<SelectListItem> GetMaritalStatus()
        {
            List<SelectListItem> items = new List<SelectListItem>()
            {
                new SelectListItem() { Text = "Single", Value = "Single" },
                new SelectListItem() { Text = "Married", Value = "Married" },
                new SelectListItem() { Text = "Divorced", Value = "Divorced" },
                new SelectListItem() { Text = "Widowed", Value = "Widowed" }
            };
            return items;
        }
        public static IEnumerable<SelectListItem> GetAgeList()
        {
            List<SelectListItem> items = new List<SelectListItem>()
            {
                new SelectListItem() { Text = "15-29", Value = "15-29" },
                new SelectListItem() { Text = "30-44", Value = "Married" },
                new SelectListItem() { Text = "45-55", Value = "56 and Over" },
               
            };
            return items;
        }
    }
}