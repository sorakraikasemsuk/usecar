using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UseCar.Models;

namespace UseCar.Helper
{
    public class DropdownList
    {
        readonly UseCarDBContext context;
        public DropdownList(UseCarDBContext context)
        {
            this.context = context;
        }
        public List<SelectListItem> DeaprtmentAll()
        {
            return (from a in context.department
                    where a.isEnable
                    select new SelectListItem {
                        Value=a.departmentId.ToString(),
                        Text=a.departmentName
                    }).ToList();
        }
        public List<SelectListItem> CategoryShopAll()
        {
            return (from a in context.categoryshop
                    where a.isEnable
                    select new SelectListItem
                    {
                        Value = a.categoryShopId.ToString(),
                        Text = a.categoryShopName
                    }).ToList();
        }
        public List<SelectListItem> BranchAll()
        {
            return (from a in context.branch
                    where a.isEnable
                    select new SelectListItem
                    {
                        Value = a.branchId.ToString(),
                        Text = a.branchName
                    }).ToList();
        }
        public List<SelectListItem> BrandAll()
        {
            return (from a in context.brand
                    where a.isEnable
                    select new SelectListItem
                    {
                        Value = a.brandId.ToString(),
                        Text = a.brandName
                    }).ToList();
        }
        public List<SelectListItem> GenerationByBrand(int brandId)
        {
            return (from a in context.generation
                    where a.isEnable
                    && a.brandId == brandId
                    select new SelectListItem
                    {
                        Value = a.generationId.ToString(),
                        Text = a.generationName
                    }).ToList();
        }
        public List<SelectListItem> FacByGeneration(int generationId)
        {
            return (from a in context.face
                    where a.isEnable
                    && a.generationId == generationId
                    select new SelectListItem
                    {
                        Value = a.faceId.ToString(),
                        Text = a.faceName
                    }).ToList();
        }
        public List<SelectListItem> SubFaceByFace(int faceId)
        {
            return (from a in context.subface
                    where a.isEnable
                    && a.faceId == faceId
                    select new SelectListItem
                    {
                        Value = a.subfaceId.ToString(),
                        Text = a.subfaceName
                    }).ToList();
        }
        public List<SelectListItem> Color()
        {
            return (from a in context.color
                    where a.isEnable
                    select new SelectListItem
                    {
                        Value = a.colorId.ToString(),
                        Text = a.colorName
                    }).ToList();
        }
        public List<SelectListItem> Gear()
        {
            return (from a in context.gear
                    where a.isEnable
                    select new SelectListItem
                    {
                        Value = a.gearId.ToString(),
                        Text = a.gearName
                    }).ToList();
        }
    }
}
