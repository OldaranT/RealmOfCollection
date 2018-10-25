﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmOfCollection.FuzzyLogic
{
    public abstract class FuzzyTerm
    {
        //all terms must implement a virtual constructor
        public abstract FuzzyTerm Clone();

        /**
         * retrieves the degree of membership of the term
         */
        public abstract double GetDOM();

        /**
         * clears the degree of membership of the term
         */
        public abstract void ClearDOM();

        /**
         * method for updating the DOM of a consequent when a rule fires
         */
        public abstract void ORwithDOM(double val);
    }
}
