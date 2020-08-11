using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using DojoDachi.Models;

namespace DojoDachi.Controllers
{
    public class HomeController : Controller
    {

        [HttpGet("")]
        public IActionResult Index()
        {
            Dachi Beemo = new Dachi("Beemo")
            {
                Name = "Beemo",
                Happiness = 20,
                Fullness = 20,
                Energy = 50,
                Meals = 3,
            };

            if (HttpContext.Session.GetInt32("Happiness") == null)
            {
                HttpContext.Session.SetInt32("Happiness", 20);
                HttpContext.Session.SetInt32("Fullness", 20);
                HttpContext.Session.SetInt32("Energy", 50);
                HttpContext.Session.SetInt32("Meals", 3);
                HttpContext.Session.SetString("Message", "I'm super cute");
            }
             if(HttpContext.Session.GetInt32("Happiness") == 0 || HttpContext.Session.GetInt32("Fullness") == 0)
            {
                ViewBag.Result = true;
                TempData["Message"] = "Dachi has died";
            }

            if(HttpContext.Session.GetInt32("Happiness") >= 100 && HttpContext.Session.GetInt32("Fullness") >= 100)
            {
                ViewBag.Result = true;
                TempData["Message"] = "You Won!";
            }

            ViewBag.Happiness = HttpContext.Session.GetInt32("Happiness");
            ViewBag.Fullness = HttpContext.Session.GetInt32("Fullness");
            ViewBag.Energy = HttpContext.Session.GetInt32("Energy");
            ViewBag.Meals = HttpContext.Session.GetInt32("Meals");
            ViewBag.Message = HttpContext.Session.GetString("Message");

            return View("Index");
        }

       
        [HttpPost("/feed")]
        public IActionResult Feed()
        {
            Random rand = new Random ();
            int Food = rand.Next(5,11);
            if (HttpContext.Session.GetInt32("Meals") <= 0)
            {
                HttpContext.Session.SetString("Message", "You can't feed your pet right now. Go to work and get some meals.");
                return RedirectToAction("Index");
            }
            else
            {
                int likeIt = rand.Next(0, 5);
                if (likeIt == 0)
                {   
                    int? StillHungry = HttpContext.Session.GetInt32("Energy") -5;  
                    HttpContext.Session.SetInt32("Energy", (int)StillHungry);
                    HttpContext.Session.SetString("Message", "Ewww! Your Dojodachi didn't like it!");
                    int? Mealcount = HttpContext.Session.GetInt32("Meals") -1; 
                    HttpContext.Session.SetInt32("Meals", (int)Mealcount);
                    return RedirectToAction ("Index");

                }
                else
                {

                    int? mealEffect = HttpContext.Session.GetInt32("Fullness") + Food; 
                    HttpContext.Session.SetInt32("Fullness",(int) mealEffect);
                    int? Mealhappycount = HttpContext.Session.GetInt32("Meals") -1; 
                    HttpContext.Session.SetInt32("Meals", (int)Mealhappycount);
                    HttpContext.Session.SetString("Message", "Thanks for the food! That was yummy!");
                    return RedirectToAction ("Index");

                }
                


            }
        }
        // public void winCheck()
        // {
        //     int? winCheckFull =  HttpContext.Session.GetInt32("Fullness");
        //     int? winCheckHapp = HttpContext.Session.GetInt32("Happiness");
        //     int? winCheckEnergy = HttpContext.Session.GetInt32("Energy");
        // if (winCheckFull >= 100 && winCheckHapp >= 100 && winCheckEnergy >= 100);
        // {
        //     HttpContext.Session.SetString("Message", "Your Dojodachi has been perfectly cared for, YOU WIN!!");
        //     return RedirectToAction("Index");
        // }
        // return RedirectToAction("Index");
        // }

        [HttpPost("/play")]
        public IActionResult Play()
            {
                Random rand = new Random();
                if (HttpContext.Session.GetInt32("Energy") <= 0)
                { 
                    HttpContext.Session.SetString("Message", "Beemo is too tired to play.  Let it rest.");
                    return RedirectToAction("Index");
                }
                else 
                {
                    int IsHappy = rand.Next(0,5);
                    if (IsHappy == 0)
                    {
                        int? PlayEnergy1 = HttpContext.Session.GetInt32("Energy") - 5; 
                        HttpContext.Session.SetInt32("Energy", (int) PlayEnergy1);
                        HttpContext.Session.SetString("Message","That wasn't fun...better luck next time");
                    }
                    int Happiness =rand.Next(5, 11);
                    int? PlayEnergy=HttpContext.Session.GetInt32("Energy") -5; 
                    int? MoreHappy=HttpContext.Session.GetInt32("Happiness") + Happiness;
                    HttpContext.Session.SetInt32("Happiness", (int)MoreHappy);
                    HttpContext.Session.SetInt32("Energy", (int)PlayEnergy);
                    HttpContext.Session.SetString("Message", "I love to play...that was super awesome");
                        
                    
                }
                return RedirectToAction("Index");
            }

        [HttpPost("/work")]
        public IActionResult Work()//Working costs 5 energy and earns between 1 and 3 meals
                {
                Random rand = new Random();
                if (HttpContext.Session.GetInt32("Energy") <= 5)
                { 
                    HttpContext.Session.SetString("Message", "Beemo is too tired to work.  You should let Beemo sleep.");
                    return RedirectToAction("Index");
                }
                else 
                {
                    int Meals = rand.Next(0,4);
                    int? Mealtotal= HttpContext.Session.GetInt32("Meals") + Meals; 
                    HttpContext.Session.SetInt32("Meals", (int)Mealtotal);
                    
                    int? Work=HttpContext.Session.GetInt32("Energy");
                    HttpContext.Session.SetInt32("Energy", (int)Work -5);
                    HttpContext.Session.SetString("Message", "Beemo is working hard for the money. Earning meals and using energy.");
                        

                    
                }
                return RedirectToAction("Index");
            }

        [HttpPost("/sleep")]
        public IActionResult Sleep()//Sleeping earns 15 energy and decreases fullness and happiness each by 5
         {
                if (HttpContext.Session.GetInt32("Energy") ==100)
                {
                    HttpContext.Session.SetString("Message", "Beemo isn't tired");
                    return RedirectToAction("Index");
                }
                else {
                    
                
                HttpContext.Session.GetInt32("Energy");
                int? Rested = HttpContext.Session.GetInt32("Energy")+15;
                HttpContext.Session.SetInt32("Energy",(int) Rested);
                int? Hunger=HttpContext.Session.GetInt32("Fullness")-5;
                HttpContext.Session.SetInt32("Fullness", (int)Hunger);
                int? Happy=HttpContext.Session.GetInt32("Happiness")-5;
                HttpContext.Session.SetInt32("Happiness", (int)Happy);
                HttpContext.Session.SetString("Message", "Beemo got some good rest and increased their energy by 15, but got 5 points hangrier and less happy.");
                

                    return RedirectToAction("Index");
                }
                   
         }
        [HttpPost("/restart")]

        public IActionResult Restart ()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }         
    }
    
    }
    

