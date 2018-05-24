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
                new SelectListItem() {Text = "Guest", Value = "Guest"},
                new SelectListItem() {Text = "Special Guest", Value = "Special Guest"}
            };
            return delegatetypes;
        }

        public static IEnumerable<SelectListItem> GetNameTitles()
        {
            List<SelectListItem> titles = new List<SelectListItem>()
            {
                new SelectListItem() {Text = "Regular", Value = "Regular" },
                new SelectListItem() {Text = "Delegate At Large", Value = "Delegate At Large"},
                new SelectListItem() {Text = "Special Delegate", Value="Special Delegate", },
                new SelectListItem() {Text = "Guest", Value = "Guest"},
                new SelectListItem() {Text = "Special Guest", Value = "Special Guest"}
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

        public static IEnumerable<SelectListItem> AgeGroup()
        {
            List<SelectListItem> items = new List<SelectListItem>()
            {
                new SelectListItem() { Text = "0-15", Value="0-15"},
                new SelectListItem() { Text = "16-35", Value="16-35"},
                new SelectListItem() { Text = "36-55", Value="36-55"},
                new SelectListItem() { Text = "55 and Over", Value="55 and Over"}
            };

            return items;
        }
    }
}