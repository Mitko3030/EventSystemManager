using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.RegularExpressions;

namespace Program
{
    public class Skateboard
    {
        private string model;
        private double price;

        public Skateboard(string model, double price)
        {
            Model = model;
            Price = price;
        }
        public string Model
        {
            get
            {
                return this.model;
            }
            set
            {
                // // Iterative way of validation
                // for (int i = 0; i < value.Length; i++)
                // {
                //     int currentCode = value[i];

                //     if ((currentCode < 65 || currentCode > 90) && (currentCode < 97 || currentCode > 122))
                //     {
                //         throw new ValidationException("Input values should be a capital or lower latin sign");
                //     }
                // }

                // // Regex validation
                // Validator.ValidateCapitalSmall(value);

                if (!Regex.IsMatch(value, "^([A-Za-z])+$"))
                {
                    throw new ValidationException("Input values should be a capital or lower latin letter!");
                }

                this.model = value;
            }
        }
        public double Price
        {
            get
            {
                return this.price;
            }
            set
            {
                this.price = value;
            }
        }

        public override string ToString()
        {
            return $"Skateboard {this.model} costs {this.price:F2} lv.";
        }

    }
}