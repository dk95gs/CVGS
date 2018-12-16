using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CVGS.Data;
using CVGS.Models;
using CVGS.Models.ViewModel;
using CVGS.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CVGS.Areas.Customer.Controllers
{
    [Authorize(Roles = SD.SuperAdminUser + ", " + SD.EmployeeUser + ", " + SD.MemberUser)]
    [Area("Customer")]
    public class CartsController : Controller
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        private CheckoutCartViewModel CheckoutCartVM { get; set; }

        [BindProperty]
        private CartViewModel CartVM { get; set; }

        public CartsController(ApplicationDbContext db)
        {
            _db = db;
            CheckoutCartVM = new CheckoutCartViewModel();
            CartVM = new CartViewModel();
        }
        public async Task<IActionResult> Index()
        {
            var rec = await _db.Carts.Include(m=>m.ApplicationUser).Where(m => m.ApplicationUser.UserName == User.Identity.Name).FirstOrDefaultAsync();
            if(rec == null)
            {
                //var userId = _db.ApplicationUser.FirstOrDefault(m => m.UserName == User.Identity.Name).Id;
                //List<Games> newCartList = new List<Games>();

                //Carts cart = new Carts()
                //{
                //    ApplicationUserId = userId,
                //    MyCartItems = JsonConvert.SerializeObject(newCartList),
                //    Total = 0
                //};
                //_db.Add(cart);
                //await _db.SaveChangesAsync();
                //rec = _db.Carts.Include(m => m.ApplicationUser).Where(m => m.ApplicationUser.UserName == User.Identity.Name).FirstOrDefault();
                //ViewBag.cartTotal = rec.Total;

                //return View(JsonConvert.DeserializeObject<List<Games>>(rec.MyCartItems));
                List<Games> tempCart = new List<Games>();
                CartVM.MyCartItems = new List<Games>();
                CartVM.FriendCartItems = new Dictionary<string, IEnumerable<Games>>();
                return View(CartVM);
            }
            ViewBag.cartTotal = rec.Total;
            IEnumerable<Games> cartList = JsonConvert.DeserializeObject<IEnumerable<Games>>(rec.MyCartItems);
            Dictionary<string, IEnumerable<Games>> friendCartList = JsonConvert.DeserializeObject<Dictionary<string, IEnumerable<Games>>>(rec.FriendsCartItems);

            CartVM.MyCartItems = cartList;
            CartVM.FriendCartItems = friendCartList;

            return View(CartVM);
        }
        
        public async Task<IActionResult> Add(int Id)
        {
            var addGame = await _db.Games.FirstOrDefaultAsync(m => m.Id == Id);
            var userCartsRec = await _db.Carts.Include(m => m.ApplicationUser).FirstOrDefaultAsync(m => m.ApplicationUser.UserName == User.Identity.Name);
            if(userCartsRec == null)
            {
                var userId = _db.ApplicationUser.FirstOrDefault(m => m.UserName == User.Identity.Name).Id;
                List<Games> newCartList = new List<Games>()
                {
                    addGame
                };
                Dictionary<string, List<Games>> friendsCart = new Dictionary<string, List<Games>>();

                Carts cart = new Carts()
                {
                    ApplicationUserId = userId,
                    MyCartItems = JsonConvert.SerializeObject(newCartList),
                    FriendsCartItems = JsonConvert.SerializeObject(friendsCart),
                    Total = addGame.Price
                };
                _db.Add(cart);
                await _db.SaveChangesAsync();
                userCartsRec = _db.Carts.Include(m => m.ApplicationUser).Where(m => m.ApplicationUser.UserName == User.Identity.Name).FirstOrDefault();
                return RedirectToAction("Index", "StoreFront", null);
            }
            var cartItems = JsonConvert.DeserializeObject<List<Games>>(userCartsRec.MyCartItems);
            cartItems.Add(addGame);
            userCartsRec.MyCartItems = JsonConvert.SerializeObject(cartItems);
            userCartsRec.Total += addGame.Price;

            await _db.SaveChangesAsync();
            return RedirectToAction("Index", "StoreFront", null);
        }
        public async Task<IActionResult> AddToFriendCart(int gameId, string userId)
        {
            var game = await _db.Games.FirstOrDefaultAsync(m => m.Id == gameId);
            var myUserId = _db.ApplicationUser.Where(m => m.UserName == User.Identity.Name).FirstOrDefault().Id;
            var userName = _db.ApplicationUser.FirstOrDefault(m => m.Id == userId).UserName;
            var userCart = await _db.Carts.FirstOrDefaultAsync(m => m.ApplicationUserId == myUserId);

            if (userCart == null)
            {
                List<Games> newCartList = new List<Games>();

                Dictionary<string, List<Games>> friendsCart = new Dictionary<string, List<Games>>();
                List<Games> newFriendGameList = new List<Games>();
                newFriendGameList.Add(game);

                friendsCart.Add(userName, newFriendGameList);

                Carts cart = new Carts()
                {
                    ApplicationUserId = myUserId,
                    MyCartItems = JsonConvert.SerializeObject(newCartList),
                    FriendsCartItems = JsonConvert.SerializeObject(friendsCart),
                    Total = game.Price
                };
                _db.Add(cart);
                await _db.SaveChangesAsync();

                return RedirectToAction("GetUserWishList", "WishLists", new { Id = userId } );
            }
            var friendGameDict = JsonConvert.DeserializeObject < Dictionary<string, List<Games>>>(userCart.FriendsCartItems);
            if (friendGameDict.ContainsKey(userName))
            {
                friendGameDict[userName].Add(game);
                userCart.Total += game.Price;
            }
            else
            {
                friendGameDict.Add(userName, new List<Games>() { game } );
                userCart.Total += game.Price;
            }
            userCart.FriendsCartItems = JsonConvert.SerializeObject(friendGameDict);
            await _db.SaveChangesAsync();
            return RedirectToAction("GetUserWishList", "WishLists", new { Id = userId });
        }
        public async Task<IActionResult> Delete (int Id)
        {
            var userCart = await _db.Carts.Include(m => m.ApplicationUser).FirstOrDefaultAsync(m => m.ApplicationUser.UserName == User.Identity.Name);

            var cartItems = JsonConvert.DeserializeObject<List<Games>>(userCart.MyCartItems);
            var friendCartItems = JsonConvert.DeserializeObject<Dictionary<string, IEnumerable<Games>>>(userCart.FriendsCartItems);
            var game = cartItems[Id];

            cartItems.RemoveAt(Id);

            userCart.MyCartItems = JsonConvert.SerializeObject(cartItems);
            userCart.Total -= game.Price;
            if (cartItems.Count == 0 && friendCartItems.Count == 0)
                userCart.Total = 0;

            ViewBag.cartTotal = userCart.Total;

            await _db.SaveChangesAsync();

            return RedirectToAction("Index", "Carts", null);
        }
        public async Task<IActionResult> DeleteFromFriendCart(int Id, string userName)
        {
            var userCart = _db.Carts.Include(m => m.ApplicationUser).FirstOrDefault(m => m.ApplicationUser.UserName == User.Identity.Name);

            var friendCart = JsonConvert.DeserializeObject < Dictionary<string, List<Games>>>(userCart.FriendsCartItems);
            var price = friendCart[userName][Id].Price;

            friendCart[userName].RemoveAt(Id);

            userCart.FriendsCartItems = JsonConvert.SerializeObject(friendCart);
            userCart.Total -= price;

            ViewBag.cartTotal = userCart.Total;

            await _db.SaveChangesAsync();
            return RedirectToAction("Index", "Carts", null);
        }
        //get
        public async Task<IActionResult> Checkout()
        {           
            var creditCardList = await _db.CreditCards.Include(m => m.ApplicationUser).Where(m => m.ApplicationUser.UserName == User.Identity.Name).ToListAsync();
            var shippingAddressList = await _db.ShippingMailingAddresses.Include(m => m.ApplicationUser).Where(m => m.ApplicationUser.UserName == User.Identity.Name).ToListAsync();
            var myCart = await _db.Carts.Include(m => m.ApplicationUser).FirstOrDefaultAsync(m => m.ApplicationUser.UserName == User.Identity.Name);

            CheckoutCartVM.MyCartItems = JsonConvert.DeserializeObject<IEnumerable<Games>>(myCart.MyCartItems);
            CheckoutCartVM.FriendsCartItems = JsonConvert.DeserializeObject<Dictionary<string,IEnumerable<Games>>>(myCart.FriendsCartItems);
            CheckoutCartVM.CreditCards = creditCardList;
            CheckoutCartVM.ShippingAddresses = shippingAddressList;

            return View(CheckoutCartVM);
        }
        
        //post
        [HttpPost, ActionName("Checkout")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CheckoutPOST(int addressId, int cardId)
        {
            try
            {
                //get & calculate data needed for checkout
                var creditCard = await _db.CreditCards.FirstOrDefaultAsync(m => m.Id == cardId);
                var SA = await _db.ShippingMailingAddresses.FirstOrDefaultAsync(m => m.Id == addressId);
                var cart = await _db.Carts.Include(m => m.ApplicationUser).FirstOrDefaultAsync(m => m.ApplicationUser.UserName == User.Identity.Name);
                var ownedGameRec = await _db.OwnedGames.Include(m => m.ApplicationUser).FirstOrDefaultAsync(m => m.ApplicationUser.UserName == User.Identity.Name);

                if (ownedGameRec == null)
                {
                    List<Games> newList = new List<Games>();
                    OwnedGames newOG = new OwnedGames()
                    {
                        ApplicationUserId = cart.ApplicationUser.Id,
                        GameList = JsonConvert.SerializeObject(newList)
                    };
                    _db.Add(newOG);
                    await _db.SaveChangesAsync();
                    ownedGameRec = await _db.OwnedGames.Include(m => m.ApplicationUser).FirstOrDefaultAsync(m => m.ApplicationUser.UserName == User.Identity.Name);
                }

                var ownedGamesList = JsonConvert.DeserializeObject<List<Games>>(ownedGameRec.GameList);
                if (ownedGamesList == null)
                {
                    ownedGamesList = new List<Games>();
                }

                var myCartItems = JsonConvert.DeserializeObject<IEnumerable<Games>>(cart.MyCartItems);
                var friendCartItems = JsonConvert.DeserializeObject<Dictionary<string, IEnumerable<Games>>>(cart.FriendsCartItems);

                var total = SD.CalculateTaxes(SA.Province, cart.Total);
                string concatedAddress = $"{SA.StreetName} Unit {SA.HouseNumber} {SA.PostalCode} {SA.Province} {SA.City} {SA.Country}";

                //Create new order
                Orders newOrder = new Orders()
                {
                    ApplicationUserId = cart.ApplicationUser.Id,
                    MyCartItems = cart.MyCartItems,
                    FriendsCartItems = cart.FriendsCartItems,
                    CartTotal = cart.Total,
                    TaxTotal = total[0],
                    CartPlusTaxTotal = total[1],
                    CreationDate = DateTime.Now,
                    ShipmentAddress = concatedAddress,
                    CreditCardHash = creditCard.CreditCardNumberHash
                };
                _db.Add(newOrder);
                await _db.SaveChangesAsync();

                //Todo: check if game is downloadable. If it is, add it to the owned games table for the corrisponding user
                foreach (var item in myCartItems)
                {
                    if (item.isDownloadable)
                    {
                        ownedGamesList.Add(item);
                    }
                }
                ownedGameRec.GameList = JsonConvert.SerializeObject(ownedGamesList);

                foreach (var item in friendCartItems)
                {
                    var user = await _db.ApplicationUser.FirstOrDefaultAsync(m => m.UserName == item.Key);
                    var userId = user.Id;
                    var friendGameRec = await _db.OwnedGames.Include(m => m.ApplicationUser).FirstOrDefaultAsync(m => m.ApplicationUser.Id == user.Id);

                    if (friendGameRec == null)
                    {
                        List<Games> gamesList = new List<Games>();
                        OwnedGames newOwnedGames = new OwnedGames()
                        {
                            ApplicationUserId = user.Id,
                            GameList = JsonConvert.SerializeObject(gamesList)
                        };
                        _db.Add(newOwnedGames);
                        await _db.SaveChangesAsync();
                        friendGameRec = await _db.OwnedGames.Include(m => m.ApplicationUser).FirstOrDefaultAsync(m => m.ApplicationUser.Id == user.Id);
                    }
                    var gameListItems = JsonConvert.DeserializeObject<List<Games>>(friendGameRec.GameList);
                    foreach (var game in item.Value)
                    {
                        if (game.isDownloadable)
                            gameListItems.Add(game);
                    }
                    var updateOwnedGames = await _db.OwnedGames.SingleOrDefaultAsync(m => m.ApplicationUserId == user.Id);
                    updateOwnedGames.GameList = JsonConvert.SerializeObject(gameListItems);
                    await _db.SaveChangesAsync();
                }
                //Todo: Add to shipments table
                Shipments newShipment = new Shipments()
                {
                    ApplicationUserId = _db.ApplicationUser.FirstOrDefault(m => m.UserName == User.Identity.Name).Id,
                    Order = JsonConvert.SerializeObject(newOrder),
                    StreetName = SA.StreetName,
                    PostalCode = SA.PostalCode,
                    Province = SA.PostalCode,
                    City = SA.City,
                    Country = SA.Country,
                    isApproved = false,
                    isProccessing = false
                };
                _db.Add(newShipment);
                _db.Remove(cart);
                await _db.SaveChangesAsync();

                return View("CheckoutSuccess");
            }
            catch (Exception)
            {
                ViewBag.checkoutFail = "Must add a shipping and/or credit cart to account";
                return RedirectToAction(nameof(Index));
            }
        }

    }
}