﻿/*
 * Copyright (c) 2015 GraphDefined GmbH
 * This file is part of WWCP OCPI <https://github.com/GraphDefined/WWCP_OCPI>
 *
 * Licensed under the Affero GPL license, Version 3.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.gnu.org/licenses/agpl.html
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

#region Usings

using System;
using System.Linq;
using System.Collections.Generic;

#endregion

namespace org.GraphDefined.WWCP.OCPI_2_0
{

    /// <summary>
    /// A charging tariff element.
    /// </summary>
    public struct TariffElement
    {

        #region Properties

        #region PriceComponents

        private readonly IEnumerable<PriceComponent> _PriceComponents;

        /// <summary>
        /// Enumeration of price components that make up the pricing of this tariff.
        /// </summary>
        public IEnumerable<PriceComponent> PriceComponents
        {
            get
            {
                return _PriceComponents;
            }
        }

        #endregion

        #region TariffRestrictions

        private readonly IEnumerable<TariffRestriction> _TariffRestrictions;

        /// <summary>
        /// Enumeration of tariff restrictions.
        /// </summary>
        public IEnumerable<TariffRestriction> TariffRestrictions
        {
            get
            {
                return _TariffRestrictions;
            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new charging tariff element.
        /// </summary>
        /// <param name="PriceComponents">Enumeration of price components that make up the pricing of this tariff.</param>
        /// <param name="TariffRestrictions">Enumeration of tariff restrictions.</param>
        public TariffElement(IEnumerable<PriceComponent>     PriceComponents,
                             IEnumerable<TariffRestriction>  TariffRestrictions)
        {

            #region Initial checks

            if (PriceComponents == null)
                throw new ArgumentNullException("PriceComponents", "The given parameter must not be null!");

            if (!PriceComponents.Any())
                throw new ArgumentNullException("PriceComponents", "The given enumeration must not be empty!");

            #endregion

            this._PriceComponents     = PriceComponents;
            this._TariffRestrictions  = TariffRestrictions;

        }

        #endregion

    }

}