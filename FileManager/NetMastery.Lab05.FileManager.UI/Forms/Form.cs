﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetMastery.Lab05.FileManager.UI.Forms
{
    public abstract class Form
    {
        public string CurrentPath { get; set; }
        public bool IsValid => Errors.Count == 0;
        public Dictionary<string, List<string>> Errors = new Dictionary<string, List<string>>();
        public virtual void RenderErrors()
        {
            foreach (var property in Errors)
            {
                Console.WriteLine($"{property.Key} errors: ");
                Console.WriteLine("----------------------");
                foreach (var error in property.Value)
                {
                    Console.WriteLine(error);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        public virtual void RenderForm()
        {
            Console.WriteLine();
        }
        public void AddError(string key, string error)
        {
            if(Errors.ContainsKey(key))
            {
                Errors[key].Add(error);
            }
            else
            {
                Errors.Add(key, new List<string>());
                Errors[key].Add(error);
            }
        }
        public void  RemoveError(string key)
        {
            if(Errors.ContainsKey(key))
            {
                Errors.Remove(key);
            }
        }
        public void RemoveAll()
        {
            Errors.Clear();
        }
    }
}