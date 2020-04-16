using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QBit_Decoder
{
    class Paquete
    {
        private string paquete;
        public Paquete(string paquete)
        {
            this.paquete = paquete;
        }

        public Dictionary<string, string> Parsear()
        {
            Dictionary<string, string> diccionarioQBit = new Dictionary<string, string>();
            Dictionary<string, string> diccionarioError = new Dictionary<string, string>();
            diccionarioError.Add("Start Bit", "E");


            string startBitKey = "Start Bit";
            string packetLengthKey = "Longitud del Paquete";
            string stopBitKey = "Stop Bit";
            string protocolNumberKey = "Tipo de Paquete";
            string startBit;
            string startBit1 = "7878";
            string startBit2 = "7979";
            string stopBit = "0D0A";
            int packetLengthBytes;
            string packetLength;

            string dateTimeKey = "Fecha y Hora";
            string mccKey = "Mobile Country Code (MCC)";
            string mncKey = "Mobile Network Code (MNC)";
            string lacKey = "Location Area Code (LAC)";
            string cellIDKey = "Cell Tower ID";
            string rssiKey = "Received Signal Stregnth Indicator";

            string dateTime;
            string mcc;
            string mnc;
            string lac;
            string cellID;
            string rssi;

            string nlac1Key = "Location Area Code 1(LAC)";
            string ncellID1Key = "Cell Tower ID 1";
            string nrssi1Key = "Received Signal Stregnth Indicator 1";
            string nlac2Key = "Location Area Code 2(LAC)";
            string ncellID2Key = "Cell Tower ID 2";
            string nrssi2Key = "Received Signal Stregnth Indicator 2";
            string nlac3Key = "Location Area Code 3(LAC)";
            string ncellID3Key = "Cell Tower ID 3";
            string nrssi3Key = "Received Signal Stregnth Indicator 3";
            string nlac4Key = "Location Area Code 4(LAC)";
            string ncellID4Key = "Cell Tower ID 4";
            string nrssi4Key = "Received Signal Stregnth Indicator 4";
            string nlac5Key = "Location Area Code 5(LAC)";
            string ncellID5Key = "Cell Tower ID 5";
            string nrssi5Key = "Received Signal Stregnth Indicator 5";
            string nlac6Key = "Location Area Code 6(LAC)";
            string ncellID6Key = "Cell Tower ID 6";
            string nrssi6Key = "Received Signal Stregnth Indicator 6";

            string satellitesKey = "Cantidad de Satélites";
            string latitudeKey = "Latitud";
            string longitudeKey = "Longitud";
            string speedKey = "Velocidad";
            string courseStatusKey = "Course, Status";

            string voltageLvlKey = "Voltage Level";
            string gsmSignalStrengthKey = "GSM Signal Strength";
            string voltageLvl;
            string gsmSignalStrength;

            string satellites;
            string latitude;
            string longitude;
            string speed;
            string courseStatus;

            string nlac1;
            string ncellID1; ;
            string nrssi1;
            string nlac2;
            string ncellID2;
            string nrssi2;
            string nlac3;
            string ncellID3;
            string nrssi3;
            string nlac4;
            string ncellID4;
            string nrssi4;
            string nlac5;
            string ncellID5;
            string nrssi5;
            string nlac6;
            string ncellID6;
            string nrssi6;

            string timeLeadsKey = "Time Leads";
            string timeLeads;

            string packetLengthChar;
            string ano;
            string mes;
            string dia;
            string hora;
            string minuto;
            string segundo;

            string satellitesLength;
            string satellitesNumber;

            string courseStatusBin;
            string courseStatus15;
            string courseStatus14;
            string courseStatus13;
            string courseStatus12;
            string course;




            if ((paquete.Contains(startBit1) || paquete.Contains(startBit2)) && paquete.Substring(paquete.Length - 4, 4) == stopBit)
            {
                if (paquete.Contains(startBit1))
                {
                    startBit = startBit1;
                    packetLengthBytes = 1;
                    int indice = paquete.IndexOf(startBit);
                    packetLength = paquete.Substring(indice + 4, packetLengthBytes * 2);
                    packetLengthChar = ((Convert.ToInt32(packetLength, 16))*2).ToString() + " caracteres";
                }
                else
                {
                    startBit = startBit2;
                    packetLengthBytes = 2;
                    packetLength = paquete.Substring(paquete.IndexOf(startBit) + 4, packetLengthBytes * 2);
                    packetLengthChar = ((Convert.ToInt32(packetLength, 16)) * 2).ToString() + " caracteres";
                }
                paquete = paquete.Substring(paquete.IndexOf(startBit));

                diccionarioQBit.Add(startBitKey, startBit);
                diccionarioQBit.Add(packetLengthKey, packetLengthChar);




                string protocolNumber = paquete.Substring(paquete.IndexOf(packetLength) + packetLengthBytes * 2, 2);
                
                if (packetLengthBytes == 1) paquete = paquete.Substring(8);
                else paquete = paquete.Substring(10);
                int posicion = 0;


                //Information Content


                switch (protocolNumber)
                {
                    case "01":  //Login Packet

                        string terminalIDKey = "Terminal ID";
                        string typeIDCodeKey = "Type Identification Code";
                        string timeZoneLanguageKey = "Time Zone Language";

                        if (paquete.Length > 14)
                        {
                            protocolNumber = "Paquete Login";

                            string terminalID = paquete.Substring(posicion, 16);
                            terminalID = terminalID.Substring(1);
                            posicion += 16;
                            string typeIDCode = paquete.Substring(posicion, 4);
                            posicion += 4;
                            string timeZoneLanguage = paquete.Substring(posicion, 4);
                            posicion += 4;

                            string timeZoneLanguageBin = String.Join(String.Empty, 
                                timeZoneLanguage.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));

                            string timezone = (Convert.ToInt32(("0" + timeZoneLanguage.Substring(0, 3)), 16)/100).ToString();

                            string gmt = timeZoneLanguageBin.Substring(timeZoneLanguageBin.Length - 4, 1);
                            if (gmt == "0") gmt = "+";
                            else gmt = "-";

                            diccionarioQBit.Add(protocolNumberKey, protocolNumber);
                            diccionarioQBit.Add(terminalIDKey, terminalID);
                            diccionarioQBit.Add(typeIDCodeKey, typeIDCode);
                            diccionarioQBit.Add(timeZoneLanguageKey, "GMT"+gmt+timezone);
                        }
                        else
                        {
                            protocolNumber = "Paquete Login (Respuesta del Servidor)";
                            diccionarioQBit.Add(protocolNumberKey, protocolNumber);
                        }
                        break;
                    case "22":  // GPS Packet



                        string accKey = "ACC Status";
                        string dataUploadModeKey = "Data Upload Mode";
                        string gpsRTRUKey = "GPS Real-Time/Re-upload";
                        string mileageKey = "Mileage";


                        protocolNumber = "Paquete GPS";

                        dateTime = paquete.Substring(posicion, 12);
                        posicion += 12;

                        ano = Convert.ToInt32(dateTime.Substring(0, 2), 16).ToString();
                        mes = Convert.ToInt32(dateTime.Substring(2, 2), 16).ToString();
                        dia = Convert.ToInt32(dateTime.Substring(4, 2), 16).ToString();
                        hora = Convert.ToInt32(dateTime.Substring(6, 2), 16).ToString();
                        minuto = Convert.ToInt32(dateTime.Substring(8, 2), 16).ToString();
                        segundo = Convert.ToInt32(dateTime.Substring(10, 2), 16).ToString();


                        satellites = paquete.Substring(posicion, 2);
                        posicion += 2;

                        satellitesLength = Convert.ToInt32(satellites.Substring(0, 1), 16).ToString();
                        satellitesNumber = Convert.ToInt32(satellites.Substring(1), 16).ToString();

                        latitude = (Convert.ToDouble(Convert.ToUInt32(paquete.Substring(posicion, 8), 16))/1800000).ToString();
                        posicion += 8;
                        longitude = (Convert.ToDouble(Convert.ToUInt32(paquete.Substring(posicion, 8), 16))/1800000).ToString();
                        posicion += 8;
                        speed = Convert.ToInt32(paquete.Substring(posicion, 2),16).ToString();
                        posicion += 2;
                        courseStatus = paquete.Substring(posicion, 4);
                        posicion += 4;

                        courseStatusBin = String.Join(String.Empty,
                                courseStatus.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
                        courseStatus15 = courseStatusBin.Substring(2, 1);
                        if (courseStatus15 == "0") courseStatus15 = "Real Time GPS. ";
                        else courseStatus15 = "Differential Positioning. ";
                        courseStatus14 = courseStatusBin.Substring(3, 1);
                        if (courseStatus14 == "0") courseStatus14 = "GPS not positioned. ";
                        else courseStatus14 = "GPS has been positioned. ";
                        courseStatus13 = courseStatusBin.Substring(4, 1);
                        if (courseStatus13 == "0")
                        {
                            courseStatus13 = "East Longitude. ";
                            //longitude = "" + longitude;
                        }
                        else
                        {
                            courseStatus13 = "West Longitud. ";
                            longitude = "-" + longitude;
                        }
                        courseStatus12 = courseStatusBin.Substring(5, 1);
                        if (courseStatus12 == "0")
                        {
                            courseStatus12 = "South Latitude. ";
                            latitude = "-" + latitude;
                        }
                        else
                        {
                            courseStatus12 = "West Longitud. ";
                            //latitude = "-" + longitude;
                        }
                        course = "Course: " + Convert.ToInt32(courseStatusBin.Substring(6),2).ToString() + "°";

                        mcc = Convert.ToInt32(paquete.Substring(posicion, 4),16).ToString();
                        posicion += 4;
                        mnc = Convert.ToInt32(paquete.Substring(posicion, 2), 16).ToString();
                        posicion += 2;
                        lac = Convert.ToInt32(paquete.Substring(posicion, 4), 16).ToString();
                        posicion += 4;
                        cellID = Convert.ToInt32(paquete.Substring(posicion, 6), 16).ToString();
                        posicion += 6;
                        string acc = paquete.Substring(posicion, 2);
                        posicion += 2;
                        if (acc == "00") acc = "Low";
                        else acc = "High";
                        
                        string dataUploadMode = paquete.Substring(posicion, 2);
                        posicion += 2;

                        switch (dataUploadMode)
                        {
                            case "00":
                                dataUploadMode = "Upload in fixed time.";
                                break;
                            case "01":
                                dataUploadMode = "Upload by fixed distance.";
                                break;
                            case "02":
                                dataUploadMode = "Inflection point upload.";
                                break;
                            case "03":
                                dataUploadMode = "ACC status upload.";
                                break;
                            case "04":
                                dataUploadMode = "Re-upload the last locating point when back to static.";
                                break;
                            case "05":
                                dataUploadMode = "Upload the last effective point when network back to normal after breaks.";
                                break;
                        }

                        string gpsRTRU = paquete.Substring(posicion, 2);
                        posicion += 2;
                        if (gpsRTRU == "00") gpsRTRU = "Real time Upload";
                        else gpsRTRU = "Re-upload";

                        string mileage = (Convert.ToInt32(paquete.Substring(posicion, 8),16)/100).ToString();
                        posicion += 4;  //TODO ver si se aplica al Qbit

                        diccionarioQBit.Add(protocolNumberKey, protocolNumber);
                        diccionarioQBit.Add(dateTimeKey, dia+"/"+mes+"/"+"20"+ano+" "+hora+":"+minuto+":"+segundo);
                        diccionarioQBit.Add(satellitesKey, "GPS information length: "+satellitesLength+"; Satellite Number: "+satellitesNumber);
                        diccionarioQBit.Add(latitudeKey, latitude);
                        diccionarioQBit.Add(longitudeKey, longitude);
                        diccionarioQBit.Add(speedKey, speed);
                        diccionarioQBit.Add(courseStatusKey, courseStatus15+courseStatus14+courseStatus13+courseStatus12+course);
                        diccionarioQBit.Add(mccKey, mcc);
                        diccionarioQBit.Add(mncKey, mnc);
                        diccionarioQBit.Add(lacKey, lac);
                        diccionarioQBit.Add(cellIDKey, cellID);
                        diccionarioQBit.Add(accKey, acc);
                        diccionarioQBit.Add(dataUploadModeKey, dataUploadMode);
                        diccionarioQBit.Add(gpsRTRUKey, gpsRTRU);
                        diccionarioQBit.Add(mileageKey, mileage);

                        break;
                    case "23":  //Heartbeat Packet

                        string terminalInfoContentKey = "Terminal Information Content";
                        string voltageLevelKey = "Voltage Level";

                        string languagePortStatusKey = "Language/Extended Port Status";

                        if (paquete.Length > 14)
                        {
                            protocolNumber = "Paquete Heartbeat";

                            string terminalInfoContent = paquete.Substring(posicion, 2);
                            posicion += 2;

                            string terminalInfoContentBin = String.Join(String.Empty,
                                terminalInfoContent.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));

                            if (terminalInfoContentBin.Substring(1, 1) == "0") terminalInfoContent = "GPS Tracking OFF. ";
                            else terminalInfoContent = "GPS Tracking ON. ";
                            /*TODO el protocolo no indica que es el extended bit
                             * if (terminalInfoContentBin.Substring.(5, 1) = "0") terminalInfoContent += "GPS Tracking OFF. ";
                            else terminalInfoContent += "GPS Tracking ON. ";*/


                            string voltageLevel = (Convert.ToDouble(Convert.ToInt32(paquete.Substring(posicion, 4),16))/100).ToString() + " V";
                            posicion += 4;
                            gsmSignalStrength = paquete.Substring(posicion, 2);
                            posicion += 2;

                            switch (gsmSignalStrength)
                            {
                                case "00":
                                    gsmSignalStrength = "No Signal.";
                                    break;
                                case "01":
                                    gsmSignalStrength = "Extremely weak signal.";
                                    break;
                                case "02":
                                    gsmSignalStrength = "Very weak signal.";
                                    break;
                                case "03":
                                    gsmSignalStrength = "Good signal.";
                                    break;
                                case "04":
                                    gsmSignalStrength = "Strong signal.";
                                    break;
                            }

                            string languagePortStatus = paquete.Substring(posicion, 4);
                            posicion += 4;
                            if (languagePortStatus.Substring(2, 2) == "02") languagePortStatus = "English";
                            else languagePortStatus = "Chinese";

                            diccionarioQBit.Add(protocolNumberKey, protocolNumber);
                            diccionarioQBit.Add(terminalInfoContentKey, terminalInfoContent);
                            diccionarioQBit.Add(voltageLevelKey, voltageLevel);
                            diccionarioQBit.Add(gsmSignalStrengthKey, gsmSignalStrength);
                            diccionarioQBit.Add(languagePortStatusKey, languagePortStatus);

                        }
                        else
                        {
                            protocolNumber = "Paquete Heartbeat (Respuesta del Servidor)";
                            diccionarioQBit.Add(protocolNumberKey, protocolNumber);
                        }
                        break;
                    case "21":  // Online command replied by terminal
                        break;
                    case "26":  //Alarm Packet GPS
                        if (paquete.Length > 14)
                        {
                            protocolNumber = "Paquete Alarma GPS";

                            dateTime = paquete.Substring(posicion, 12);
                            posicion += 12;

                            ano = Convert.ToInt32(dateTime.Substring(0, 2), 16).ToString();
                            mes = Convert.ToInt32(dateTime.Substring(2, 2), 16).ToString();
                            dia = Convert.ToInt32(dateTime.Substring(4, 2), 16).ToString();
                            hora = Convert.ToInt32(dateTime.Substring(6, 2), 16).ToString();
                            minuto = Convert.ToInt32(dateTime.Substring(8, 2), 16).ToString();
                            segundo = Convert.ToInt32(dateTime.Substring(10, 2), 16).ToString();


                            satellites = paquete.Substring(posicion, 2);
                            posicion += 2;

                            satellitesLength = Convert.ToInt32(satellites.Substring(0, 1), 16).ToString();
                            satellitesNumber = Convert.ToInt32(satellites.Substring(1), 16).ToString();

                            latitude = (Convert.ToDouble(Convert.ToUInt32(paquete.Substring(posicion, 8), 16)) / 1800000).ToString();
                            posicion += 8;
                            longitude = (Convert.ToDouble(Convert.ToUInt32(paquete.Substring(posicion, 8), 16)) / 1800000).ToString();
                            posicion += 8;
                            speed = Convert.ToInt32(paquete.Substring(posicion, 2), 16).ToString();
                            posicion += 2;
                            courseStatus = paquete.Substring(posicion, 4);
                            posicion += 4;

                            courseStatusBin = String.Join(String.Empty,
                                    courseStatus.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
                            courseStatus15 = courseStatusBin.Substring(2, 1);
                            if (courseStatus15 == "0") courseStatus15 = "Real Time GPS. ";
                            else courseStatus15 = "Differential Positioning. ";
                            courseStatus14 = courseStatusBin.Substring(3, 1);
                            if (courseStatus14 == "0") courseStatus14 = "GPS not positioned. ";
                            else courseStatus14 = "GPS has been positioned. ";
                            courseStatus13 = courseStatusBin.Substring(4, 1);
                            if (courseStatus13 == "0")
                            {
                                courseStatus13 = "East Longitude. ";
                                //longitude = "" + longitude;
                            }
                            else
                            {
                                courseStatus13 = "West Longitud. ";
                                longitude = "-" + longitude;
                            }
                            courseStatus12 = courseStatusBin.Substring(5, 1);
                            if (courseStatus12 == "0")
                            {
                                courseStatus12 = "South Latitude. ";
                                latitude = "-" + latitude;
                            }
                            else
                            {
                                courseStatus12 = "West Longitud. ";
                            }
                            course = "Course: " + Convert.ToInt32(courseStatusBin.Substring(6), 2).ToString() + "°";


                            string lbsLengthKey = "LBS length in total";
                            string lbsLength = Convert.ToInt32(paquete.Substring(posicion, 2), 16).ToString();
                            posicion += 2;

                            mcc = Convert.ToInt32(paquete.Substring(posicion, 4), 16).ToString();
                            posicion += 4;
                            mnc = Convert.ToInt32(paquete.Substring(posicion, 2), 16).ToString();
                            posicion += 2;
                            lac = Convert.ToInt32(paquete.Substring(posicion, 4), 16).ToString();
                            posicion += 4;
                            cellID = Convert.ToInt32(paquete.Substring(posicion, 6), 16).ToString();
                            posicion += 6;

                            string terminalInfoKey = "Terminal information";
                            string terminalInfo = paquete.Substring(posicion, 2);
                            posicion += 2;

                            string terminalInfoBin = String.Join(String.Empty,
                                    terminalInfo.Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));

                            if (terminalInfoBin.Substring(0, 1) == "0") terminalInfo = "Oil and electricity connected. ";
                            else terminalInfo = "Oil and electricity disconnected. ";
                            if (terminalInfoBin.Substring(1, 1) == "0") terminalInfo += "GPS tracking is OFF. ";
                            else terminalInfo += "GPS tracking is ON. ";
                            switch(terminalInfoBin.Substring(2, 3))
                            {
                                case "100": terminalInfo += "SOS. ";
                                    break;
                                case "011":
                                    terminalInfo += "Low Battery Alarm. ";
                                    break;
                                case "010":
                                    terminalInfo += "Power Cut Alarm. ";
                                    break;
                                case "001":
                                    terminalInfo += "Vibration Alarm. ";
                                    break;
                                case "000":
                                    terminalInfo += "Normal. ";
                                    break;
                            }


                            if (terminalInfoBin.Substring(5, 1) == "0") terminalInfo += "Not Charge. ";
                            else terminalInfo += "Charging. ";
                            if (terminalInfoBin.Substring(6, 1) == "0") terminalInfo += "ACC Low. ";
                            else terminalInfo += "ACC High. ";
                            if (terminalInfoBin.Substring(7, 1) == "0") terminalInfo += "Defense Deactivated.";
                            else terminalInfo += "Defense Activated. ";

                            voltageLvl = paquete.Substring(posicion, 2);
                            posicion += 2;

                            switch(voltageLvl)
                            {
                                case "00": voltageLvl = "No Power (shutdown)";
                                    break;
                                case "01":
                                    voltageLvl = "Extremely Low Battery (not enough for calling or sending text messages, etc.)";
                                    break;
                                case "02":
                                    voltageLvl = "Very Low Battery (Low Battery Alarm)";
                                    break;
                                case "03":
                                    voltageLvl = "Low Battery (can be used normally)";
                                    break;
                                case "04":
                                    voltageLvl = "Medium";
                                    break;
                                case "05":
                                    voltageLvl = "High";
                                    break;;
                                case "06":
                                    voltageLvl = "Very High";
                                    break;
                            }

                            gsmSignalStrength = paquete.Substring(posicion, 2);
                            posicion += 2;

                            switch (gsmSignalStrength)
                            {
                                case "00":
                                    gsmSignalStrength = "No Signal.";
                                    break;
                                case "01":
                                    gsmSignalStrength = "Extremely weak signal.";
                                    break;
                                case "02":
                                    gsmSignalStrength = "Very weak signal.";
                                    break;
                                case "03":
                                    gsmSignalStrength = "Good signal.";
                                    break;
                                case "04":
                                    gsmSignalStrength = "Strong signal.";
                                    break;
                            }

                            string alarmLanguageKey = "Alarm/Language";
                            string alarmLanguage = paquete.Substring(posicion, 4);
                            posicion += 4;

                            switch (alarmLanguage.Substring(0,2))
                            {
                                case "00":
                                    alarmLanguage = "Normal";
                                    break;
                                case "01":
                                    alarmLanguage = "SOS";
                                    break;
                                case "02":
                                    alarmLanguage = "Power Cut Alarm";
                                    break;
                                case "03": 
                                    alarmLanguage = "Vibration Alarm";
                                    break;
                                case "04":
                                    alarmLanguage = "Enter Fence alarm";
                                    break;
                                case "05":
                                    alarmLanguage = "Exit fence alarm";
                                    break;
                                case "06":
                                    alarmLanguage = "Over Speed alarm";
                                    break;
                                case "09":
                                    alarmLanguage = "Vibration Alarm";
                                    break;
                                case "0A":
                                    alarmLanguage = "Enter GPS dead zone alarm";
                                    break;
                                case "0B":
                                    alarmLanguage = "Exit GPS dead zone alarm";
                                    break;
                                case "0C":
                                    alarmLanguage = "Power on alarm";
                                    break;
                                case "0D":
                                    alarmLanguage = "GPS first fix notice";
                                    break;
                                case "0E":
                                    alarmLanguage = "Low battery alarm";
                                    break;
                                case "0F":
                                    alarmLanguage = "Low battery protection alarm";
                                    break;
                                case "10":
                                    alarmLanguage = "SIM change notice";
                                    break;
                                case "11":
                                    alarmLanguage = "Power off alarm";
                                    break;
                                case "12":
                                    alarmLanguage = "Airplane mode alarm";
                                    break;
                                case "13":
                                    alarmLanguage = "Disassemble alarm";
                                    break;
                                case "14":
                                    alarmLanguage = "Door alarm";
                                    break;
                                case "15":
                                    alarmLanguage = "Low battery and shutdown alarm";
                                    break;
                                case "16":
                                    alarmLanguage = "Sound control alarm";
                                    break;
                                case "17":
                                    alarmLanguage = "Pseudo base-station alarm";
                                    break;
                            }
                            switch (alarmLanguage.Substring(2, 2))
                            {
                                case "01":
                                    alarmLanguage += ". Language Chinese.";
                                    break;
                                case "02":
                                    alarmLanguage += ". Language English.";
                                    break;
                            }

                            diccionarioQBit.Add(protocolNumberKey, protocolNumber);

                            diccionarioQBit.Add(dateTimeKey, dia + "/" + mes + "/" + "20" + ano + " " + hora + ":" + minuto + ":" + segundo);
                            diccionarioQBit.Add(satellitesKey, "GPS information length: " + satellitesLength + "; Satellite Number: " + satellitesNumber);
                            diccionarioQBit.Add(latitudeKey, latitude);
                            diccionarioQBit.Add(longitudeKey, longitude);
                            diccionarioQBit.Add(speedKey, speed);
                            diccionarioQBit.Add(courseStatusKey, courseStatus);
                            diccionarioQBit.Add(lbsLengthKey, lbsLength);
                            diccionarioQBit.Add(mccKey, mcc);
                            diccionarioQBit.Add(mncKey, mnc);
                            diccionarioQBit.Add(lacKey, lac);
                            diccionarioQBit.Add(cellIDKey, cellID);
                            diccionarioQBit.Add(terminalInfoKey, terminalInfo);
                            diccionarioQBit.Add(voltageLvlKey, voltageLvl);
                            diccionarioQBit.Add(gsmSignalStrengthKey, gsmSignalStrength);
                            diccionarioQBit.Add(alarmLanguageKey, alarmLanguage);


                        }
                        else
                        {
                            protocolNumber += " - Paquete Alarma GPS (Respuesta del Servidor)";
                            diccionarioQBit.Add(protocolNumberKey, protocolNumber);
                        }

                        break;
                    case "28":  //Multiple Bases Extension Information Packet -- no existen el logs de prueba

                        protocolNumber += " - Paquete LBS";

                        dateTime = paquete.Substring(posicion, 12);
                        posicion += 12;
                        mcc = paquete.Substring(posicion, 4);
                        posicion += 4;
                        mnc = paquete.Substring(posicion, 2);
                        posicion += 2;
                        lac = paquete.Substring(posicion, 4);
                        posicion += 4;
                        cellID = paquete.Substring(posicion, 6);
                        posicion += 6;
                        rssi = paquete.Substring(posicion, 2);
                        posicion += 2;

                        nlac1 = paquete.Substring(posicion, 4);
                        posicion += 4;
                        ncellID1 = paquete.Substring(posicion, 6);
                        posicion += 6;
                        nrssi1 = paquete.Substring(posicion, 2);
                        posicion += 2;
                        nlac2 = paquete.Substring(posicion, 4);
                        posicion += 4;
                        ncellID2 = paquete.Substring(posicion, 6);
                        posicion += 6;
                        nrssi2 = paquete.Substring(posicion, 2);
                        posicion += 2;
                        nlac3 = paquete.Substring(posicion, 4);
                        posicion += 4;
                        ncellID3 = paquete.Substring(posicion, 6);
                        posicion += 6;
                        nrssi3 = paquete.Substring(posicion, 2);
                        posicion += 2;
                        nlac4 = paquete.Substring(posicion, 4);
                        posicion += 4;
                        ncellID4 = paquete.Substring(posicion, 6);
                        posicion += 6;
                        nrssi4 = paquete.Substring(posicion, 2);
                        posicion += 2;
                        nlac5 = paquete.Substring(posicion, 4);
                        posicion += 4;
                        ncellID5 = paquete.Substring(posicion, 6);
                        posicion += 6;
                        nrssi5 = paquete.Substring(posicion, 2);
                        posicion += 2;
                        nlac6 = paquete.Substring(posicion, 4);
                        posicion += 4;
                        ncellID6 = paquete.Substring(posicion, 6);
                        posicion += 6;
                        nrssi6 = paquete.Substring(posicion, 2);
                        posicion += 2;


                        timeLeads = paquete.Substring(posicion, 2);
                        posicion += 2;
                        string languageKey = "Idioma";
                        string language = paquete.Substring(posicion, 2);
                        posicion += 2;

                        diccionarioQBit.Add(protocolNumberKey, protocolNumber);
                        diccionarioQBit.Add(dateTimeKey, dateTime);
                        diccionarioQBit.Add(mccKey, mcc);
                        diccionarioQBit.Add(mncKey, mnc);
                        diccionarioQBit.Add(lacKey, lac);
                        diccionarioQBit.Add(cellIDKey, cellID);
                        diccionarioQBit.Add(rssiKey, rssi);
                        diccionarioQBit.Add(lacKey + " 1", nlac1);
                        diccionarioQBit.Add(cellIDKey + " 1", ncellID1);
                        diccionarioQBit.Add(rssiKey + " 1", nrssi1);
                        diccionarioQBit.Add(lacKey + " 2", nlac2);
                        diccionarioQBit.Add(cellIDKey + " 2", ncellID2);
                        diccionarioQBit.Add(rssiKey + " 2", nrssi2);
                        diccionarioQBit.Add(lacKey + " 3", nlac3);
                        diccionarioQBit.Add(cellIDKey + " 3", ncellID3);
                        diccionarioQBit.Add(lacKey + " 3", nlac3);
                        diccionarioQBit.Add(cellIDKey + " 4", ncellID4);
                        diccionarioQBit.Add(lacKey + " 5", nlac5);
                        diccionarioQBit.Add(cellIDKey + " 5", ncellID5);
                        diccionarioQBit.Add(rssiKey + " 5", nrssi5);
                        diccionarioQBit.Add(lacKey + " 6", nlac6);
                        diccionarioQBit.Add(cellIDKey + " 6", ncellID6);
                        diccionarioQBit.Add(rssiKey + " 6", nrssi6);
                        diccionarioQBit.Add(rssiKey + " 1", nrssi1);
                        diccionarioQBit.Add(rssiKey + " 1", nrssi1);
                        diccionarioQBit.Add(timeLeadsKey, timeLeads);
                        diccionarioQBit.Add(languageKey, language);

                        break;
                    case "80":  //  Online Command -- no se encuentran en log de prueba
                        break;
                    case "8A":  //  Time Packet

                        if (paquete.Length > 14)
                        {
                            protocolNumber = "Paquete Time. Server Response";

                            dateTime = paquete.Substring(posicion, 12);
                            posicion += 12;
                            ano = Convert.ToInt32(dateTime.Substring(0, 2), 16).ToString();
                            mes = Convert.ToInt32(dateTime.Substring(2, 2), 16).ToString();
                            dia = Convert.ToInt32(dateTime.Substring(4, 2), 16).ToString();
                            hora = Convert.ToInt32(dateTime.Substring(6, 2), 16).ToString();
                            minuto = Convert.ToInt32(dateTime.Substring(8, 2), 16).ToString();
                            segundo = Convert.ToInt32(dateTime.Substring(10, 2), 16).ToString();


                            diccionarioQBit.Add(protocolNumberKey, protocolNumber);
                            diccionarioQBit.Add(dateTimeKey, dia + "/" + mes + "/" + "20" + ano + " " + hora + ":" + minuto + ":" + segundo);

                        }
                        else
                        {
                            protocolNumber = "Paquete Time. Time request sent by terminal";
                            diccionarioQBit.Add(protocolNumberKey, protocolNumber);
                        }

                        break;
                    case "94":  //  Information Transmission Packet --- 7979

                        protocolNumber += " - Paquete Information Transmission. Datos de No Posición.";
                        diccionarioQBit.Add(protocolNumberKey, protocolNumber);
                        /*string infoTypeKey = "Tipo de Información";
                        string infoType = paquete.Substring(posicion, 2);
                        posicion += 2;
                        string contenidoKey = "Contenido";
                        string contenido = */


                        break;
                    case "19":  //  Alarm Packet LBS    -- no se encuentran en log de prueba
                        break;
                    case "2C":

                        protocolNumber += " - Paquete WiFi";

                        dateTime = paquete.Substring(posicion, 12);
                        posicion += 12;
                        ano = Convert.ToInt32(dateTime.Substring(0, 2), 16).ToString();
                        mes = Convert.ToInt32(dateTime.Substring(2, 2), 16).ToString();
                        dia = Convert.ToInt32(dateTime.Substring(4, 2), 16).ToString();
                        hora = Convert.ToInt32(dateTime.Substring(6, 2), 16).ToString();
                        minuto = Convert.ToInt32(dateTime.Substring(8, 2), 16).ToString();
                        segundo = Convert.ToInt32(dateTime.Substring(10, 2), 16).ToString();

                        diccionarioQBit.Add(protocolNumberKey, protocolNumber);
                        diccionarioQBit.Add(dateTimeKey, dia + "/" + mes + "/" + "20" + ano + " " + hora + ":" + minuto + ":" + segundo);


                        mcc = Convert.ToInt32(paquete.Substring(posicion, 4), 16).ToString();
                        posicion += 4;
                        mnc = Convert.ToInt32(paquete.Substring(posicion, 2), 16).ToString();
                        posicion += 2;
                        lac = Convert.ToInt32(paquete.Substring(posicion, 4), 16).ToString();
                        posicion += 4;
                        cellID = Convert.ToInt32(paquete.Substring(posicion, 6), 16).ToString();
                        posicion += 6;

                        rssi = Convert.ToInt32(paquete.Substring(posicion, 2), 16).ToString() + " (rango de 0 a 255)";
                        posicion += 2;

                        nlac1 = Convert.ToInt32(paquete.Substring(posicion, 4), 16).ToString();
                        posicion += 4;

                        ncellID1 = Convert.ToInt32(paquete.Substring(posicion, 6), 16).ToString();
                        posicion += 6;
                        nrssi1 = Convert.ToInt32(paquete.Substring(posicion, 2), 16).ToString();
                        posicion += 2;
                        nlac2 = Convert.ToInt32(paquete.Substring(posicion, 4), 16).ToString();
                        posicion += 4;
                        ncellID2 = Convert.ToInt32(paquete.Substring(posicion, 6), 16).ToString();
                        posicion += 6;
                        nrssi2 = Convert.ToInt32(paquete.Substring(posicion, 2), 16).ToString();
                        posicion += 2;
                        nlac3 = Convert.ToInt32(paquete.Substring(posicion, 4), 16).ToString();
                        posicion += 4;
                        ncellID3 = Convert.ToInt32(paquete.Substring(posicion, 6), 16).ToString();
                        posicion += 6;
                        nrssi3 = Convert.ToInt32(paquete.Substring(posicion, 2), 16).ToString();
                        posicion += 2;
                        nlac4 = Convert.ToInt32(paquete.Substring(posicion, 4), 16).ToString();
                        posicion += 4;
                        ncellID4 = Convert.ToInt32(paquete.Substring(posicion, 6), 16).ToString();
                        posicion += 6;
                        nrssi4 = Convert.ToInt32(paquete.Substring(posicion, 2), 16).ToString();
                        posicion += 2;
                        nlac5 = Convert.ToInt32(paquete.Substring(posicion, 4), 16).ToString();
                        posicion += 4;
                        ncellID5 = Convert.ToInt32(paquete.Substring(posicion, 6), 16).ToString();
                        posicion += 6;
                        nrssi5 = Convert.ToInt32(paquete.Substring(posicion, 2), 16).ToString();
                        posicion += 2;
                        nlac6 = Convert.ToInt32(paquete.Substring(posicion, 4), 16).ToString();
                        posicion += 4;
                        ncellID6 = Convert.ToInt32(paquete.Substring(posicion, 6), 16).ToString();
                        posicion += 6;
                        nrssi6 = Convert.ToInt32(paquete.Substring(posicion, 2), 16).ToString();
                        posicion += 2;
                        timeLeads = Convert.ToInt32(paquete.Substring(posicion, 2), 16).ToString();
                        posicion += 2;

                        diccionarioQBit.Add(mccKey, mcc);
                        diccionarioQBit.Add(mncKey, mnc);
                        diccionarioQBit.Add(lacKey, lac);
                        diccionarioQBit.Add(cellIDKey, cellID);
                        diccionarioQBit.Add(rssiKey, rssi);
                        diccionarioQBit.Add(lacKey + " 1", nlac1);
                        diccionarioQBit.Add(cellIDKey + " 1", ncellID1);
                        diccionarioQBit.Add(rssiKey + " 1", nrssi1);
                        diccionarioQBit.Add(lacKey + " 2", nlac2);
                        diccionarioQBit.Add(cellIDKey + " 2", ncellID2);
                        diccionarioQBit.Add(rssiKey + " 2", nrssi2);
                        diccionarioQBit.Add(lacKey + " 3", nlac3);
                        diccionarioQBit.Add(cellIDKey + " 3", ncellID3);
                        diccionarioQBit.Add(rssiKey + " 3", nrssi3);
                        diccionarioQBit.Add(cellIDKey + " 4", ncellID4);
                        diccionarioQBit.Add(lacKey + " 5", nlac5);
                        diccionarioQBit.Add(cellIDKey + " 5", ncellID5);
                        diccionarioQBit.Add(rssiKey + " 5", nrssi5);
                        diccionarioQBit.Add(lacKey + " 6", nlac6);
                        diccionarioQBit.Add(cellIDKey + " 6", ncellID6);
                        diccionarioQBit.Add(rssiKey + " 6", nrssi6);

                        diccionarioQBit.Add(timeLeadsKey, timeLeads);

                        string wifiQuantityKey = "Wifi Quantity";
                        string wifiQuantity = Convert.ToInt32(paquete.Substring(posicion, 2), 16).ToString();
                        posicion += 2;

                        diccionarioQBit.Add(wifiQuantityKey, wifiQuantity);

                        // Problemas con este tipo de paquete
                        // 2020/03/07 | 17:39:26 | 172.30.88.52:55050: | < | 78786B2C14030714282902D2361BE7008F1F1E1BE7005095111BE70040FF0C000000000000000000000000000000000000000000000000FF07087E64AAE1683498DED05A232B44C4EA1DDC0067455A90435E893553D87D7F23C68E54AC84C6D50859545890435E883354000451FA0D0A

                        /*if (Convert.ToInt32(wifiQuantity) > 0)
                        {
                            string wifiMac1Key = "WiFi MAC ";
                            string wifiStrength1Key = "WiFi Strength of signal ";
                            string wifiSSIDLength1Key = "SSID Length of SSID ";
                            string wifiSSID1Key = "SSID ";

                            string wifiMac; //TODO preguntar si la mac debe convertirse a decimal
                            string wifiStrength;
                            string wifiSSIDLength;
                            string wifiSSID;

                            for (int i = 0; i < Convert.ToInt32(wifiQuantity,16); i++)
                            {
                                wifiMac1Key += (i + 1).ToString();
                                wifiStrength1Key += (i + 1).ToString();
                                wifiSSIDLength1Key += (i + 1).ToString() + " WiFi";
                                wifiSSID1Key += (i + 1).ToString();

                                //wifiMac = Convert.ToInt32(paquete.Substring(posicion, 12), 16).ToString();
                                wifiMac = paquete.Substring(posicion, 12);
                                posicion += 12;

                                wifiStrength = Convert.ToInt32(paquete.Substring(posicion, 2), 16).ToString();
                                posicion += 2;
                                wifiSSIDLength = Convert.ToInt32(paquete.Substring(posicion, 2), 16).ToString();
                                posicion += 2;
                                //wifiSSID = Convert.ToInt32(paquete.Substring(posicion, Convert.ToInt32(wifiSSIDLength)*2), 16).ToString();
                                wifiSSID = paquete.Substring(posicion, Convert.ToInt32(wifiSSIDLength) * 2);
                                posicion += Convert.ToInt32(wifiSSIDLength)*2;

                                diccionarioQBit.Add(wifiMac1Key, wifiMac);
                                diccionarioQBit.Add(wifiStrength1Key, wifiStrength);
                                diccionarioQBit.Add(wifiSSIDLength1Key, wifiSSIDLength);
                                diccionarioQBit.Add(wifiSSID1Key, wifiSSID);

                            }
                        }*/
                        


                        break;
                    case "8D":  //iBeacon Data Information Packet  -- no se ven en logs de pruebas

                        break;

                }

                string infoSerialNumberKey = "Information Serial Number";
                string errorCheckKey = "Error Check";

                string infoSerialNumber = paquete.Substring(posicion, 4);
                posicion += 4;
                string errorCheck = paquete.Substring(posicion, 4);
                posicion += 4;

                diccionarioQBit.Add(infoSerialNumberKey, infoSerialNumber);
                diccionarioQBit.Add(errorCheckKey, errorCheck);

                return diccionarioQBit;
            }
            else return diccionarioError;

        }



        }
}
