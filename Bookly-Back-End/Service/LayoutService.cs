﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bookly_Back_End.DAL;
using Bookly_Back_End.Models;
using Bookly_Back_End.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Bookly_Back_End.Service
{
    public class LayoutService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContext;
        private readonly UserManager<AppUser> _userManager;

        public LayoutService(AppDbContext context,IHttpContextAccessor httpContext,UserManager<AppUser> userManager)
        {
            _context = context;
            _httpContext = httpContext;
            _userManager = userManager;
        }

        public async Task<List<Setting>> GetData()
        {
            List<Setting> settings = await _context.Settings.ToListAsync();
            return settings;
        }

        public async Task<BasketVM> GetBasket()
        {
            string basketStr = _httpContext.HttpContext.Request.Cookies["Basket"];
            BasketVM basketData = new BasketVM();
            if (!string.IsNullOrEmpty(basketStr))
            {
                List<BasketCookieItemVM> basket = JsonConvert.DeserializeObject<List<BasketCookieItemVM>>(basketStr);
                var query = _context.Books.Include(i => i.BookImages).Include(b => b.Discount).AsQueryable();

                foreach(BasketCookieItemVM item in basket)
                {
                    Book existedBook = query.FirstOrDefault(b => b.Id == item.Id);

                    if(existedBook != null)
                    {
                        BasketItemVM basketItem = new BasketItemVM
                        {
                            Book = existedBook,
                            Count = item.Count
                        };

                        basketData.BasketItemVMs.Add(basketItem);
                    }
                }
                decimal total = default;
                foreach(BasketItemVM itemVM in basketData.BasketItemVMs)
                {
                    total += itemVM.Book.DiscountId == null ? itemVM.Book.Price * itemVM.Count : (itemVM.Book.Price - ((itemVM.Book.Price * itemVM.Book.Discount.DiscountPercent) / 100)) * itemVM.Count;
                }
                basketData.TotalPrice = total;
                basketData.Count = basketData.BasketItemVMs.Count;

                return basketData;
            }
            else
            {
                return null;
            }
        }

        public async Task<List<BasketItem>> UserBasket(string username)
        {
            List<BasketItem> baskets = await _context.BasketItems.Include(b => b.Book.BookImages).Include(b => b.AppUser)
                .Include(b => b.Book.Discount).Where(b => b.AppUser.UserName == username).ToListAsync();
            return baskets;
            //AppUser appUser = await _userManager.Users.Include(b => b.BasketItems).FirstOrDefaultAsync();
            //return appUser;
        }
        public async Task<List<WishListItem>> UserWishList(string username)
        {
            List<WishListItem> wishLists = await _context.WishListItems.Include(b => b.Book)
                .ThenInclude(b => b.Discount).Include(b => b.Book.BookImages)
                .Include(b => b.AppUser).Where(b => b.AppUser.UserName == username).ToListAsync();

            return wishLists;
        }
    }
}
