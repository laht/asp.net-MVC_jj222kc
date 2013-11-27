using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NumberGuessingGame.ViewModels;
using NumberGuessingGame.Models;

namespace NumberGuessingGame.Controllers
{
    public class SecretNumberController : Controller
    {
        private const string SECRETNUMBER = "SecretNumberModel";

        //
        // GET: /SecretNumber/

        [HttpGet]
        public ActionResult Index()
        {
            //Create session object if null
            if (Session[SECRETNUMBER] == null)
            {
                Session[SECRETNUMBER] = new SecretNumber();
            }
            //Get the secrectNumber model
            var secretNumber = (SecretNumber)Session[SECRETNUMBER];
            
            //Create a viewModel
            var viewModel = new SecrectNumberViewModel();

            //Set viewModel properties to match the model

            //Help the view determine if a guess is possible
            viewModel.CanMakeGuess = secretNumber.CanMakeGuess;
            //If CanMakeGuess is false the number will represent the secret number
            viewModel.Number = secretNumber.Number;
            //The last guess made
            viewModel.Lastguess = secretNumber.LastGuessedNumber;
            //List containing all guesses made
            viewModel.GuessedNumbers = secretNumber.GuessedNumbers;

            return View(viewModel);
        }

        //
        // POST: /SecretNumber/

        [HttpPost]
        public ActionResult Index(SecrectNumberViewModel viewModel)
        {
            if (Session.IsNewSession)
            {
                return View("SessionEndedView");
            }

            var secretNumber = (SecretNumber)Session[SECRETNUMBER];

            //If user has entered valid data make a guess
            if (ModelState.IsValid)
            {
                //make guess with user input
                secretNumber.MakeGuess(viewModel.Guess);
            }

            //Set viewModel properties to match the model

            //Help the view determine if a guess is possible
            viewModel.CanMakeGuess = secretNumber.CanMakeGuess;
            //If CanMakeGuess is false the number will represent the secret number
            viewModel.Number = secretNumber.Number;
            //The last guess made
            viewModel.Lastguess = secretNumber.LastGuessedNumber;
            //List containing all guesses made
            viewModel.GuessedNumbers = secretNumber.GuessedNumbers;
            return View(viewModel);
        }

        //Start new game
        [HttpGet]
        public RedirectResult newGame() 
        {
            Session[SECRETNUMBER] = null;
            return Redirect("~/");
        }

    }
}
