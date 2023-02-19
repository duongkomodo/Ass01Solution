﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models {
    public class DataProvider {
        private static DataProvider _instance;
        public static DataProvider Instance {
            get {
                if (_instance == null)
                    _instance = new DataProvider();
                return _instance;
            }
            set {
                _instance = value;
            }
        }

        public FStoreContext DB {
            get; set;
        }
        public DataProvider() {
            DB = new FStoreContext();
        }

        public void SaveChange() {
            DB.SaveChanges();
        }
    }
}