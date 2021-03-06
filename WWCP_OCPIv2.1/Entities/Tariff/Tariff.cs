﻿/*
 * Copyright (c) 2015-2016 GraphDefined GmbH
 * This file is part of WWCP OCPI <https://github.com/GraphDefined/WWCP_OCPI>
 *
 * Licensed under the Affero GPL license Version 3.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing software
 * distributed under the License is distributed on an "AS IS" BASIS
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

#region Usings

using System;
using System.Linq;
using System.Collections.Generic;

using org.GraphDefined.Vanaheimr.Illias;
using Newtonsoft.Json.Linq;
using org.GraphDefined.Vanaheimr.Hermod;

#endregion

namespace org.GraphDefined.WWCP.OCPIv2_1
{

    /// <summary>
    /// A Tariff Object consists of a list of one or more TariffElements,
    /// these elements can be used to create complex Tariff structures.
    /// When the list of elements contains more then 1 element, then the
    /// first tariff in the list with matching restrictions will be used.
    /// </summary>
    public class Tariff : AEMobilityEntity<Tariff_Id>,
                          IEquatable<Tariff>, IComparable<Tariff>, IComparable
    {

        #region Properties

        /// <summary>
        /// ISO 4217 code of the currency used for this tariff.
        /// </summary>
        [Mandatory]
        public Currency                    Currency         { get; }

        /// <summary>
        /// List of multi-language alternative tariff info text.
        /// </summary>
        [Mandatory]
        public I18NString                  TariffText       { get; }

        /// <summary>
        /// Alternative URL to tariff info.
        /// </summary>
        [Optional]
        public Uri                         TariffUrl        { get; }

        /// <summary>
        /// An enumeration of tariff elements.
        /// </summary>
        [Mandatory]
        public IEnumerable<TariffElement>  TariffElements   { get; }

        /// <summary>
        /// The energy mix.
        /// </summary>
        [Optional]
        public EnergyMix                   EnergyMix        { get;  }

        #endregion

        #region Constructor(s)

        #region Tariff(Id, ..., params TariffElements)

        /// <summary>
        /// Create a new tariff object.
        /// </summary>
        /// <param name="Id">Uniquely identifies the Tariff within the CPOs platform (and suboperator platforms).</param>
        /// <param name="Currency">ISO 4217 code of the currency used for this tariff.</param>
        /// <param name="TariffElements">An enumeration of tariff elements.</param>
        /// <param name="TariffText">List of multi-language alternative tariff info text.</param>
        /// <param name="TariffUrl">Alternative URL to tariff info.</param>
        public Tariff(Tariff_Id               Id,
                      Currency                Currency,
                      I18NString              TariffText,
                      Uri                     TariffUrl,
                      params TariffElement[]  TariffElements)

            : this(Id,
                   Currency,
                   TariffElements,
                   TariffText,
                   TariffUrl)

        { }

        #endregion

        #region Tariff(Id, Currency, TariffElements, TariffText = null, TariffUrl = null, EnergyMix = null)

        /// <summary>
        /// Create a new tariff object.
        /// </summary>
        /// <param name="Id">Uniquely identifies the Tariff within the CPOs platform (and suboperator platforms).</param>
        /// <param name="Currency">ISO 4217 code of the currency used for this tariff.</param>
        /// <param name="TariffElements">An enumeration of tariff elements.</param>
        /// <param name="TariffText">List of multi-language alternative tariff info text.</param>
        /// <param name="TariffUrl">Alternative URL to tariff info.</param>
        /// <param name="EnergyMix">The energy mix.</param>
        public Tariff(Tariff_Id                   Id,
                      Currency                    Currency,
                      IEnumerable<TariffElement>  TariffElements,
                      I18NString                  TariffText  = null,
                      Uri                         TariffUrl   = null,
                      EnergyMix                   EnergyMix   = null)

            : base(Id)

        {

            #region Initial checks

            if (TariffElements == null)
                throw new ArgumentNullException(nameof(TariffElements),  "The given parameter must not be null!");

            if (!TariffElements.Any())
                throw new ArgumentNullException(nameof(TariffElements),  "The given enumeration must not be empty!");

            #endregion

            #region Init data and properties

            this.Currency        = Currency;
            this.TariffElements  = TariffElements;
            this.TariffText      = TariffText != null ? TariffText : new I18NString();
            this.TariffUrl       = TariffUrl;
            this.EnergyMix       = EnergyMix;

            #endregion

        }

        #endregion

        #endregion


        #region ToJSON()

        /// <summary>
        /// Return a JSON representation of this object.
        /// </summary>
        public JObject ToJSON()

            => JSONObject.Create(new JProperty("id",        Id.ToString()),
                                 new JProperty("currency",  Currency.ISOCode),
                                 TariffText != null && TariffText.Any() ? JSONHelper.ToJSON("tariff_alt_text", TariffText)      : null,
                                 TariffUrl  != null                     ? new JProperty("tariff_alt_url", TariffUrl.ToString()) : null,
                                 TariffElements.Any()
                                     ? new JProperty("elements",    new JArray(TariffElements.Select(TariffElement => TariffElement.ToJSON())))
                                     : null,
                                 EnergyMix != null
                                     ? new JProperty("energy_mix",  EnergyMix.ToJSON())
                                     : null);

        #endregion


        #region IComparable<Tariff> Members

        #region CompareTo(Object)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Object">An object to compare with.</param>
        public Int32 CompareTo(Object Object)
        {

            if (Object == null)
                throw new ArgumentNullException("The given object must not be null!");

            // Check if the given object is an Tariff.
            var Tariff = Object as Tariff;
            if ((Object) Tariff == null)
                throw new ArgumentException("The given object is not an Tariff!");

            return CompareTo(Tariff);

        }

        #endregion

        #region CompareTo(Tariff)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Tariff">An Tariff to compare with.</param>
        public Int32 CompareTo(Tariff Tariff)
        {

            if ((Object) Tariff == null)
                throw new ArgumentNullException(nameof(Tariff),  "The given tariff must not be null!");

            return Id.CompareTo(Tariff.Id);

        }

        #endregion

        #endregion

        #region IEquatable<Tariff> Members

        #region Equals(Object)

        /// <summary>
        /// Compares two instances of this object.
        /// </summary>
        /// <param name="Object">An object to compare with.</param>
        /// <returns>true|false</returns>
        public override Boolean Equals(Object Object)
        {

            if (Object == null)
                return false;

            // Check if the given object is an Tariff.
            var Tariff = Object as Tariff;
            if ((Object) Tariff == null)
                return false;

            return this.Equals(Tariff);

        }

        #endregion

        #region Equals(Tariff)

        /// <summary>
        /// Compares two Tariffs for equality.
        /// </summary>
        /// <param name="Tariff">An Tariff to compare with.</param>
        /// <returns>True if both match; False otherwise.</returns>
        public Boolean Equals(Tariff Tariff)
        {

            if ((Object) Tariff == null)
                return false;

            return Id.Equals(Tariff.Id);

        }

        #endregion

        #endregion

        #region GetHashCode()

        /// <summary>
        /// Get the hashcode of this object.
        /// </summary>
        public override Int32 GetHashCode()

            => Id.GetHashCode();

        #endregion

        #region (override) ToString()

        /// <summary>
        /// Get a string representation of this object.
        /// </summary>
        public override String ToString()

            => Id.ToString();

        #endregion

    }

}
