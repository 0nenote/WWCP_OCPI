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

using org.GraphDefined.Vanaheimr.Illias;

#endregion

namespace org.GraphDefined.WWCP.OCPI_2_0
{

    /// <summary>
    /// A tariff restrictions class.
    /// </summary>
    public class TariffRestriction
    {

        #region Properties

        #region Time

        private readonly StartEndTime? _Time;

        /// <summary>
        /// Start/end time of day, for example "13:30 - 19:45", valid from this time of the day.
        /// </summary>
        public StartEndTime? Time
        {
            get
            {
                return _Time;
            }
        }

        #endregion

        #region Date

        private readonly StartEndDateTime? _Date;

        /// <summary>
        /// Start/end date, for example: 2015-12-24, valid from this day until that day (excluding that day).
        /// </summary>
        public StartEndDateTime? Date
        {
            get
            {
                return _Date;
            }
        }

        #endregion

        #region kWh

        private readonly DecimalMinMax? _kWh;

        /// <summary>
        /// Minimum/Maximum used energy in kWh, for example 20, valid from this amount of energy is used.
        /// </summary>
        public DecimalMinMax? kWh
        {
            get
            {
                return _kWh;
            }
        }

        #endregion

        #region Power

        private readonly DecimalMinMax? _Power;

        /// <summary>
        /// Minimum/Maximum power in kW, for example 0, valid from this charging speed.
        /// </summary>
        public DecimalMinMax? Power
        {
            get
            {
                return _Power;
            }
        }

        #endregion

        #region Duration

        private readonly TimeSpanMinMax? _Duration;

        /// <summary>
        /// Minimum/Maximum duration in seconds, valid for a duration from x seconds.
        /// </summary>
        public TimeSpanMinMax? Duration
        {
            get
            {
                return _Duration;
            }
        }

        #endregion

        #region DayOfWeek

        private readonly IEnumerable<DayOfWeek> _DayOfWeek;

        /// <summary>
        /// Minimum/Maximum duration in seconds, valid for a duration from x seconds.
        /// </summary>
        public IEnumerable<DayOfWeek> DayOfWeek
        {
            get
            {
                return _DayOfWeek;
            }
        }

        #endregion

        #endregion

        #region Constructor(s)

        /// <summary>
        /// Create a new tariff restrictions class.
        /// </summary>
        /// <param name="Time">Start/end time of day, for example "13:30 - 19:45", valid from this time of the day.</param>
        /// <param name="Date">Start/end date, for example: 2015-12-24, valid from this day until that day (excluding that day).</param>
        /// <param name="kWh">Minimum/Maximum used energy in kWh, for example 20, valid from this amount of energy is used.</param>
        /// <param name="Power">Minimum/Maximum power in kW, for example 0, valid from this charging speed.</param>
        /// <param name="Duration">Minimum/Maximum duration in seconds, valid for a duration from x seconds.</param>
        /// <param name="DayOfWeek">Minimum/Maximum duration in seconds, valid for a duration from x seconds.</param>
        public TariffRestriction(StartEndTime?           Time,
                                 StartEndDateTime?       Date,
                                 DecimalMinMax?          kWh,
                                 DecimalMinMax?          Power,
                                 TimeSpanMinMax?         Duration,
                                 IEnumerable<DayOfWeek>  DayOfWeek = null)
        {

            #region Initial checks

            if (!Time.   HasValue &&
                !Date.   HasValue &&
                !kWh.    HasValue &&
                Power.   HasValue &&
                Duration.HasValue &&
                DayOfWeek == null)
                throw new ArgumentNullException("All given parameter equals null is invalid!");

            #endregion

            this._Time       = Time;
            this._Date       = Date;
            this._kWh        = kWh;
            this._Power      = Power;
            this._Duration   = Duration;
            this._DayOfWeek  = DayOfWeek != null ? DayOfWeek.Distinct() : null;

        }

        #endregion

    }

}