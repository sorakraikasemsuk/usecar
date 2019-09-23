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
        public List<SelectListItem> Seat()
        {
            return (from a in context.seat
                    where a.isEnable
                    select new SelectListItem
                    {
                        Value = a.seatId.ToString(),
                        Text = a.seatName
                    }).ToList();
        }
        public List<SelectListItem> DriveSystem()
        {
            return (from a in context.drivesystem
                    where a.isEnable
                    select new SelectListItem
                    {
                        Value = a.driveSystemId.ToString(),
                        Text = a.driveSystemName
                    }).ToList();
        }
        public List<SelectListItem> EngineType()
        {
            return (from a in context.enginetype
                    where a.isEnable
                    select new SelectListItem
                    {
                        Value = a.engineTypeId.ToString(),
                        Text = a.engineTypeName
                    }).ToList();
        }
        public List<SelectListItem> CapacityEngine()
        {
            return (from a in context.capacityengine
                    where a.isEnable
                    select new SelectListItem
                    {
                        Value = a.capacityEngineId.ToString(),
                        Text = a.capacityEngineName
                    }).ToList();
        }
        public List<SelectListItem> Type()
        {
            return (from a in context.type
                    where a.isEnable
                    select new SelectListItem
                    {
                        Value = a.typeId.ToString(),
                        Text = a.typeName
                    }).ToList();
        }
        public List<SelectListItem> NatureByType(int typeId)
        {
            return (from a in context.nature
                    where a.isEnable
                    && a.typeId == typeId
                    select new SelectListItem
                    {
                        Value = a.typeId.ToString(),
                        Text = a.natureName
                    }).ToList();
        }
        public List<SelectListItem> ReceiveCarStatus()
        {
            return (from a in context.m_receivecare_status
                    where a.isEnable
                    select new SelectListItem
                    {
                        Value = a.receiveCarStatusId.ToString(),
                        Text = a.statusName
                    }).ToList();
        }
        public List<SelectListItem> CarStatus()
        {
            return (from a in context.m_carstatus
                    where a.isEnable
                    select new SelectListItem
                    {
                        Value = a.carStatusId.ToString(),
                        Text = a.statusName
                    }).ToList();
        }
        public List<SelectListItem> Category()
        {
            return (from a in context.category
                    where a.isEnable
                    select new SelectListItem
                    {
                        Value = a.categoryId.ToString(),
                        Text = a.categoryName
                    }).ToList();
        }
        public List<SelectListItem> Year()
        {
            List<SelectListItem> year = new List<SelectListItem>();
            int CurrentYear = DateTime.Now.Year;
            int PastYear = DateTime.Now.AddYears(-9).Year;
            for(int i= PastYear;i<= CurrentYear; i++)
            {
                year.Add(new SelectListItem
                {
                    Value = i.ToString(),
                    Text = i.ToString()
                });
            }
            return year.OrderByDescending(o=>o.Value).ToList();
        }
        public List<SelectListItem> Vendor()
        {
            return (from a in context.vendor
                    where a.isEnable
                    select new SelectListItem
                    {
                        Value = a.vendorId.ToString(),
                        Text = a.vendorName
                    }).ToList();
        }
        public List<SelectListItem> VendorWithOther()
        {
            var vendor = (from a in context.vendor
                          where a.isEnable
                          select new SelectListItem
                          {
                              Value = a.vendorId.ToString(),
                              Text = a.vendorName
                          }).ToList();
            var other = new List<SelectListItem> {
                new SelectListItem
                {
                    Value="9999",
                    Text="อิ่นๆ"
                }
            };
            return vendor.Union(other).ToList();
        }
    }
}
