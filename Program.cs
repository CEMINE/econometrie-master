﻿using MathNet.Numerics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
//using MathNet.Numerics;

namespace econometrie
{
    class Program
    {
        static void Main(string[] args)
        {

            int n = 0;
            List<double> xi = new List<double>();
            List<double> yi = new List<double>();
            List<double> zi = new List<double>();
            List<double> cadran1 = new List<double>();
            List<double> cadran2 = new List<double>();
            List<double> cadran3 = new List<double>();
            List<double> cadran4 = new List<double>();
            double media_xi = 0;
            double media_yi = 0;
            double media_zi = 0;
            double coef_a = 0;
            double coef_b = 0;
            double suma_xi = 0;
            double suma_yi = 0;
            double suma_zi = 0;
            double suma_x_patrat = 0;
            double suma_y_patrat = 0;
            double suma_z_patrat = 0;
            double suma_x_y = 0;
            double suma_z_y = 0;
            double suma_y_estimat = 0;
            double suma_y_minus_y_est_patrat = 0;
            double probabilitate = 0;
            double coef_corelatie = 0;
            double s_epsilon = 0;
            double det_a = 0;
            double det_m = 0;
            double a_est = 0;
            double det_b = 0;
            double b_est = 0;
            double k = 1;
            double sa = 0;
            double sb = 0;
            double t_critic = 0;
            double ta = 0;
            double tb = 0;
            double coef_det = 0;
            double suma_t_patrat = 0;
            double suma_t_y = 0;
            double suma_ti = 0;
            double suma_y_est_minus_y_med_patrat = 0;
            double suma_y_minus_y_med_patrat = 0;
            double t_final = 0;
            double suma_yt = 0;
            double suma_yt_minus_unu = 0;
            double suma_yt_minus_unu_patrat = 0;
            double suma_yt_minus_unu_yt = 0 ;
            List<double> x_patrat = new List<double>();
            List<double> z_patrat = new List<double>();
            List<double> y_patrat = new List<double>();
            List<double> x_times_y = new List<double>();
            List<double> z_times_y = new List<double>();
            List<double> y_estimat = new List<double>();
            List<double> y_minus_y_est_patrat = new List<double>();
            List<double> y_est_minus_y_med_patrat = new List<double>();
            List<double> ti = new List<double>();
            List<double> t_patrat = new List<double>();
            List<double> t_y = new List<double>();
            List<double> yt = new List<double>();
            List<double> yt_minus_unu = new List<double>();
            List<double> yt_minus_unu_patrat = new List<double>();
            List<double> yt_minus_unu_yt = new List<double>();
            string raspuns_cautare;




            void PLS()
            {
                            Console.WriteLine("Cate perioade are exercitiul (n) ? ");
            n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Introdu datele pentru xi");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("Introdu numarul " + i);
                xi.Add(Convert.ToDouble(Console.ReadLine()));
            }
            Console.WriteLine("\n \n Introdu datele pentru yi");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("Introdu numarul " + i);
                yi.Add(Convert.ToDouble(Console.ReadLine()));
            }
 
            for (int x = 0; x < n; x++)
            {
                //calculeaza suma numerelor din xi si yi
                media_xi += xi[x];
                media_yi += yi[x];
                suma_xi += xi[x];
                suma_yi += yi[x];
                x_patrat.Add(xi[x] * xi[x]);
                x_times_y.Add(xi[x] * yi[x]);
                y_patrat.Add(yi[x] * yi[x]);
 
                //calculeaza suma x patrat si suma x * y
                suma_x_patrat += x_patrat[x];
                suma_y_patrat += y_patrat[x];
 
                suma_x_y += x_times_y[x];
                if (x == n-1)
                {
                    // calculeaza media aritmetica pentru xi si yi
                    media_xi = media_xi / n;
                    media_yi = media_yi / n;
 
                    Console.WriteLine("Media xi este " + media_xi);
                    Console.WriteLine("Media yi este " + media_yi);
                }
            }
 
            for (int i = 0; i < n; i++)
            {
                if (xi[i] > media_xi && yi[i] > media_yi)
                {
                    cadran1.Add(1);
                    //Console.WriteLine("numar adaugat in cadran 1");
                }
                if (xi[i] < media_xi && yi[i] > media_yi)
                {
                    cadran2.Add(1);
                    //Console.WriteLine("numar adaugat in cadran 2");
 
                }
                if (xi[i] < media_xi && yi[i] < media_yi)
                {
                    cadran3.Add(1);
                    //Console.WriteLine("numar adaugat in cadran 3");
 
                }
                if (xi[i] > media_xi && yi[i] < media_yi)
                {
                    //Console.WriteLine("numar adaugat in cadran 4");
                    cadran4.Add(1);
                }
            }
            Console.WriteLine("In cadranul 1 sunt " + cadran1.Count + " numere.");
            Console.WriteLine("In cadranul 2 sunt " + cadran2.Count + " numere.");
            Console.WriteLine("In cadranul 3 sunt " + cadran3.Count + " numere.");
            Console.WriteLine("In cadranul 4 sunt " + cadran4.Count + " numere.");
 
            double procentaj_cadran1 = 0;
            double procentaj_cadran2 = 0;
            double procentaj_cadran3 = 0; 
            double procentaj_cadran4 = 0;
            //determina procentajul pentru fiecare pereche(intersectie a punctelor xi si yi) a perioadei
            double increment_procentaj = 100 / n;
 
            // afla ce procentaj este in fiecare cadran
            procentaj_cadran1 = cadran1.Count * increment_procentaj;
            procentaj_cadran2 = cadran2.Count * increment_procentaj;
            procentaj_cadran3 = cadran3.Count * increment_procentaj;
            procentaj_cadran4 = cadran4.Count * increment_procentaj;
 
            Console.WriteLine("In cadranul 1 sunt " + procentaj_cadran1 + "% din n.");
            Console.WriteLine("In cadranul 2 sunt " + procentaj_cadran2 + "% din n.");
            Console.WriteLine("In cadranul 3 sunt " + procentaj_cadran3 + "% din n.");
            Console.WriteLine("In cadranul 4 sunt " + procentaj_cadran4 + "% din n.");
 
 
            //determina tipul legaturii
            if (procentaj_cadran1 >= 30 && procentaj_cadran3 >= 30)
            {
                Console.WriteLine("Corelatia este puternica si pozitiva");
            }
            if (procentaj_cadran2 >= 30 && procentaj_cadran4 >= 30)
            {
                Console.WriteLine("Corelatia este puternica si negativa");
            }
 
            if (procentaj_cadran1 < 30 && procentaj_cadran2 < 30 && procentaj_cadran3 < 30 && procentaj_cadran4 < 30)
            {
                Console.WriteLine("Corelatia intre variabilele analizate nu are o legatura semnificativa");
            }
 
 
            //calculeaza coef a si b
            //coef_a = (suma_x_patrat * suma_yi - suma_xi * suma_x_y) / (n * suma_x_patrat - suma_xi * suma_xi );
            // coef_b = (n * suma_x_y - suma_xi* suma_yi) / (n * suma_x_patrat - suma_xi * suma_xi);
            //Console.WriteLine("Coef a este egal cu " + coef_a);
            //Console.WriteLine("Coef b este egal cu " + coef_b);
            CalculCoefA();
            CalculCoefB();
            void CalculCoefA()
            {
                coef_a = (suma_x_patrat * suma_yi - suma_xi * suma_x_y) / (n * suma_x_patrat - suma_xi * suma_xi);
                Console.WriteLine("Coef a este egal cu " + coef_a);
            }
            void CalculCoefB()
            {
                coef_b = (n * suma_x_y - suma_xi * suma_yi) / (n * suma_x_patrat - suma_xi * suma_xi);
                Console.WriteLine("Coef b este egal cu " + coef_b);
            }
            Console.WriteLine("Y = " + coef_a + " + " + coef_b + " * X + epsilon");
            Console.WriteLine($"Y mediu = {coef_a} + {coef_b} * X");
            Console.WriteLine(" \n \n Ai intrebare in care ti se modifica X-ul? y/n");
            string raspuns = Console.ReadLine().ToString();
            bool x_modificabil;
            double val_noua_x = 0;
            if (raspuns == "y" )
            {         
                Console.WriteLine("Introdu valoarea pe care o sa o ia X");
                val_noua_x = Convert.ToDouble(Console.ReadLine());
                double rez = coef_a + (coef_b * val_noua_x);
                Console.WriteLine($"Y estimat = {coef_a} + {coef_b} * {val_noua_x} = {rez}");
 
            }
            
 
            void CoefCorelatie()
            {
                double numitor_coef_corelatie;
                double numarator_coef_corelatie;
                numitor_coef_corelatie = n * suma_x_y - suma_xi * suma_yi;
                numarator_coef_corelatie = Math.Sqrt((n * suma_x_patrat - Math.Pow(suma_xi,2)) * (n * suma_y_patrat - Math.Pow(suma_yi,2)));
                coef_corelatie = numitor_coef_corelatie / numarator_coef_corelatie;
                Console.WriteLine($"Coeficientul de corelatie ( p(r)) = {coef_corelatie}");
                //Console.WriteLine($"Numitor = {n} * {suma_x_y} - {suma_xi} * {suma_yi}");
                //Console.WriteLine($"Numarator = radical ({n} * {suma_x_patrat} - {suma_x_patrat}) * ({n} * {suma_y_patrat} - {suma_y_patrat})");
                if (coef_corelatie <= -0.79)
                {
                    Console.WriteLine("Legatura este puternica si in sens contrar(negativ)");
                }
                else if (coef_corelatie >= 0.8)
                {
                    Console.WriteLine("Legatura este puternica si de acelasi sens");
                }
                else
                {
                    Console.WriteLine("Nu exista legatura intre X si Y");
                }
            }
            CoefCorelatie();
            
            void SEpsilon()
            {
                for (int i = 0; i < n; i++)
                {
                    y_estimat.Add(coef_a + coef_b * xi[i]);
                    suma_y_estimat += y_estimat[i];
                    y_minus_y_est_patrat.Add(Math.Pow(yi[i] - y_estimat[i], 2));
                    suma_y_minus_y_est_patrat += y_minus_y_est_patrat[i];
                }
                s_epsilon = suma_y_minus_y_est_patrat / (n - k - 1);
                s_epsilon = Math.Sqrt(s_epsilon);
                Console.WriteLine($"S epsilon = {s_epsilon}");             
            }
            SEpsilon();
            void SA ()
            {
                sa = s_epsilon * Math.Sqrt((suma_x_patrat) / (n * suma_x_patrat - Math.Pow(suma_xi, 2)));
                Console.WriteLine($"S a(abatere standard a coeficientului a) = {sa}");
            }
            void SB ()
            {
                sb = s_epsilon * Math.Sqrt(n / (n * suma_x_patrat - Math.Pow(suma_xi, 2)));
                Console.WriteLine($"S b(abatere standard a coeficientului b) = {sb}");
            }
            SA();
            SB();
            Console.WriteLine("Se cunoaste valoarea t-ului critic? y/n");
            string answer2 = Console.ReadLine().ToString();
            if (answer2 == "y")
            {
                Console.WriteLine("Introduceti valoarea lui t critic: ");
                t_critic = Convert.ToDouble(Console.ReadLine());
            }
            else if (answer2 == "n")
            {
 
                Console.WriteLine("Introduceti probabilitatea(ex 0.95): ");
                probabilitate = Convert.ToDouble(Console.ReadLine());
                /*
                if (probabilitate < 1)
                {
                    t_critic = MathNet.Numerics.ExcelFunctions.TInv((2 * probabilitate), n - 1);
                }
                if (probabilitate > 1 )
                {
                    t_critic = MathNet.Numerics.ExcelFunctions.TInv((2 * (probabilitate / 100)), n - 1);
                }
                */
                t_critic = Math.Abs(t_critic);
                Console.WriteLine($"t critic {t_critic}");
            }
 
            void CalculTA()
            {
                ta = Math.Abs(coef_a) / sa;
                Console.WriteLine($"T a = {ta}");
            }
            void CalculTB()
            {
                tb = Math.Abs(coef_b) / sb;
                Console.WriteLine($"T a = {tb}");
            }
            CalculTA();
            CalculTB();
            if (ta < t_critic && tb < t_critic)
            {
                Console.WriteLine("ipoteza nula se accepta, adică se afirma cu probabilitatea p că cei doi coeficienți sunt în realitate nuli");
            }
            if (ta > t_critic && tb > t_critic )
            {
                Console.WriteLine("ipoteza nula se respinge, adică se admite cu probabilitatea p că cei doi coeficienți sunt în realitate nenuli");
            }
            if (ta > t_critic && tb < t_critic)
            {
                Console.WriteLine($"Putem afirma cu o probabilitate de {probabilitate}% ca tb nu este nenul iar ta este un estimator corect");
            }
            if (ta < t_critic && tb > t_critic)
            {
                Console.WriteLine($"Putem afirma cu o probabilitate de {probabilitate}% ca ta nu este nenul iar tb este un estimator corect");
            }
            void CalculCoefDet()
            {
                for (int i = 0; i < n; i++)
                {
                    y_est_minus_y_med_patrat.Add(Math.Pow(y_estimat[i] - media_yi, 2));
                    suma_y_est_minus_y_med_patrat += y_est_minus_y_med_patrat[i];
                    suma_y_minus_y_med_patrat += Math.Pow(yi[i] - media_yi, 2);
                }
                coef_det = suma_y_est_minus_y_med_patrat / suma_y_minus_y_med_patrat;
                Console.WriteLine($"Coeficientul de determinatie (R patrat) = {coef_det}");
                Console.WriteLine($"Putem spune ca {coef_det * 100}% din variatia lui Y se datoreaza X-ului");
            }
                CalculCoefDet();
                Console.ReadLine();

            }          

            void PLSH()
            {
                Console.WriteLine("Cate perioade are exercitiul (n) ? ");
                n = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Introdu datele pentru xi");
                for (int i = 0; i < n; i++)
                {
                    Console.WriteLine("Introdu numarul " + i);
                    xi.Add(Convert.ToDouble(Console.ReadLine()));
                }
                Console.WriteLine("\n \n Introdu datele pentru yi");
                for (int i = 0; i < n; i++)
                {
                    Console.WriteLine("Introdu numarul " + i);
                    yi.Add(Convert.ToDouble(Console.ReadLine()));
                }


                for (int i = 0; i<n;i++)
                {
                    suma_yi += yi[i];
                    zi.Add(1 / xi[i]);
                    z_patrat.Add(Math.Pow(zi[i], 2));
                    y_patrat.Add(Math.Pow(yi[i],2));
                    suma_y_patrat += y_patrat[i];
                    suma_z_y += zi[i] * yi[i];
                    media_yi += yi[i];
                    suma_zi+= zi[i];
                    suma_z_patrat += z_patrat[i];
                    z_times_y.Add(zi[i] * yi[i]);
                    
                    //Console.WriteLine($"Zi{i} = {zi[i]} Yi{i} = {yi[i]} zi*yi = {z_times_y[i]}");
                    if (i == n -1) 
                    {
                        media_yi = media_yi / n;
                        media_zi= media_zi / n;
                    }

                }
                Console.WriteLine("Media yi = " + media_yi);
                double det_m = 0;
                void Det_M()
                {
                    det_m = n * suma_z_patrat - Math.Pow(suma_zi, 2);
                    Console.WriteLine("Determinantul M (det M) = " + det_m);
                }
                Det_M();
                double det_ma = 0;
                void Det_MA()
                { 
                    det_ma = suma_z_patrat * suma_yi - suma_zi * suma_z_y;
                    Console.WriteLine("Determinantul M a (det m a) = " + det_ma);
                    //Console.WriteLine("Suma z patrat = " + suma_z_patrat);
                    //Console.WriteLine($"suma yi = {suma_yi}");
                    //Console.WriteLine($"suma zi = {suma_zi}");
                    //Console.WriteLine($"suma z * y = {suma_z_y}");
                }
                Det_MA();

                double det_mb = 0;
                void Det_MB()
                {
                    det_mb = n * suma_z_y - suma_zi * suma_yi;
                    Console.WriteLine("Determinantul M b (det m b) = " + det_mb);
                    //Console.WriteLine($"n = {n}; suma z*y = {suma_z_y}; suma zi = {suma_zi}; suma yi = {suma_yi}");
                }
                Det_MB();

                double a_est = 0;
                void A_Est()
                {
                    a_est = det_ma / det_m;
                    Console.WriteLine("A estimat = " + a_est);
                    
                }
                A_Est();

                double b_est = 0;
                void B_Est()
                {
                    b_est = det_mb / det_m;
                    Console.WriteLine("B estimat = " + b_est + ". Valoarea cu care se modifica X atunci cand 1/costuri se modifica cu o unitate");
                }
                B_Est();

                for (int i = 0; i<n; i++) 
                {
                    y_estimat.Add(a_est + b_est * zi[i]);
                    suma_y_estimat += y_estimat[i];
                    suma_y_minus_y_est_patrat += Math.Pow(yi[i] - y_estimat[i], 2);
                    suma_y_est_minus_y_med_patrat += Math.Pow(y_estimat[i] - media_yi, 2);
                    suma_y_minus_y_med_patrat += Math.Pow(yi[i] - media_yi, 2);
                    //Console.WriteLine($"(Y - Y MED)^2{i} = " + (yi[i] - media_yi) * (yi[i] - media_yi));
                    //Console.WriteLine($"yi{i} = {yi[i]}; media yi = {media_yi}");


                }
                double r = 0;
                void Coef_Corelatie()
                {
                    r = (n * suma_z_y - suma_zi * suma_yi) / Math.Sqrt((n * suma_z_patrat - Math.Pow(suma_zi,2)) * (n * suma_y_patrat - Math.Pow(suma_yi,2)));
                    Console.WriteLine("Coef de corelatie (r) = " + r);
                }
                Coef_Corelatie();

                double s_epsilon = 0;
                void S_Epsilon()
                {
                    s_epsilon = Math.Sqrt((suma_y_minus_y_est_patrat / (n - 1 - k)));
                    Console.WriteLine("S epsilon = " + s_epsilon);
                }
                S_Epsilon();

                double sa = 0;
                void SA()
                {
                    sa = s_epsilon * Math.Sqrt(suma_z_patrat / (n * suma_z_patrat - Math.Pow(suma_zi, 2)));
                    Console.WriteLine("S a = " + sa);
                }
                SA();

                double sb = 0;
                void SB()
                {
                    sb = s_epsilon * Math.Sqrt(n / (n * suma_z_patrat - Math.Pow(suma_zi, 2)));
                    Console.WriteLine("S b = " + sb);
                }
                SB();
                double t_critic = 0;
                Console.WriteLine("Introduceti t-ul critic(t-ul tabelar): ");
                t_critic = Convert.ToDouble(Console.ReadLine());
                double ta = 0, tb = 0;
                void TA()
                {
                    ta = Math.Abs(a_est / sa);
                    Console.WriteLine("T a = " + ta);
                }
                TA();
                void TB()
                {
                    tb = b_est / sb;
                    Console.WriteLine("T b  = " + tb);
                }
                TB();
                if (ta < t_critic && tb < t_critic)
                {
                    Console.WriteLine("ipoteza nula se accepta, adică se afirma cu probabilitatea p că cei doi coeficienți sunt în realitate nuli");
                }
                if (ta > t_critic && tb > t_critic)
                {
                    Console.WriteLine("ipoteza nula se respinge, adică se admite cu probabilitatea p că cei doi coeficienți sunt în realitate nenuli");
                }
                if (ta > t_critic && tb < t_critic)
                {
                    Console.WriteLine($"Putem afirma cu o probabilitate de {probabilitate}% ca tb nu este nenul iar ta este un estimator corect");
                }
                if (ta < t_critic && tb > t_critic)
                {
                    Console.WriteLine($"Putem afirma cu o probabilitate de {probabilitate}% ca ta nu este nenul iar tb este un estimator corect");
                }
                double coef_det = 0;
                void Coef_Det()
                {
                    coef_det = 1 - suma_y_minus_y_est_patrat / suma_y_minus_y_med_patrat;
                    Console.WriteLine("Coeficientul de determinatie = " + coef_det);
                    //Console.WriteLine($"(y - y est)^2 = {suma_y_minus_y_est_patrat}; (y - y med)^2 = {suma_y_minus_y_med_patrat}");
                }
                Coef_Det();
                Console.ReadLine();

            }

            void PLSPartial()
            {
                Console.WriteLine("0. Se cunoaste valoarea lui n? Pune 0 daca nu.");
                n = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("1. Se cunoaste suma lui X? Pune valoarea 0 daca nu.");
                suma_xi = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("2. Se cunoaste suma lui Y? Pune valoarea 0 daca nu.");
                suma_yi = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("3. Se cunoaste valorea lui X^2? Pune valoarea 0 daca nu.");
                suma_x_patrat = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("4. Se cunoaste valoarea lui Y^2? Pune valoarea 0 daca nu.");
                suma_y_patrat = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("5. Se cunoaste suma produsului X * Y? Pune valoarea 0 daca nu.");
                suma_x_y = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("6. Se cunoaste suma lui Y estimat? Pune valoarea 0 daca nu.");
                suma_y_estimat = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("7. Se cunoastea suma lui (Y-Y estimat)^2? Pune valoarea 0 daca nu.");
                suma_y_minus_y_est_patrat = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("8. Se cunoaste suma lui (Y-Y mediu)^2 ? Pune valoarea 0 daca nu.");
                suma_y_minus_y_med_patrat = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("9. Se cunoaste suma lui (Y estimat - Y mediu) ^2 ? Pune valoarea 0 daca nu.");
                suma_y_est_minus_y_med_patrat = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("10. Se cunoaste coeficientul de corelatie (r)? Pune valoarea 0 daca nu.");
                coef_corelatie = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("11. Se cunoaste S epsilon? Pune valoarea 0 daca nu.");
                s_epsilon = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("12. Se cunoastea valoarea lui s a? Pune valoarea 0 daca nu.");
                sa = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("13. Se cunoaste valoarea lui s b? Pune valoarea 0 daca nu.");
                sb = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("14. Se cunoaste valoarea lui T a ? Pune valoarea 0 daca nu.");
                ta = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("15. Se cunoaste valoarea lui T b ? Pune valoarea 0 daca nu.");
                tb  = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("16. Se cunoaste t-ul critic? Pune valoarea 0 daca nu.");
                t_critic = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("17. Se cunoaste coeficientul de determinatie (R^2)? Pune valoarea 0 daca nu.");
                coef_det = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("18. Se cunoaste determinatul lui a? Pune valoarea 0 daca nu.");
                det_a= Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("19. Se cunoaste determinatul lui M? Pune valoarea 0 daca nu.");
                det_m= Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("20. Se cunoaste determinatul lui b? Pune valoarea 0 daca nu.");
                det_b = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("21. Se cunoaste media lui Y? Pune valoarea 0 daca nu.");
                media_yi= Convert.ToDouble(Console.ReadLine());

                

                s_epsilon = Math.Sqrt((suma_y_minus_y_est_patrat) / (n - 2));
                if (s_epsilon != 0) Console.WriteLine($"S epsilon este: {s_epsilon}");

                sa = s_epsilon * Math.Sqrt((suma_x_patrat) / (n * suma_x_patrat - Math.Pow(suma_xi, 2)));
                if (sa != 0) Console.WriteLine($"Sa este: {sa}");

                sb = s_epsilon * Math.Sqrt((n) / (n * suma_x_patrat - Math.Pow(suma_xi, 2)));
                if (sb != 0) Console.WriteLine($"Sb este: {sb}");

                det_a = suma_x_patrat * suma_yi - suma_xi * suma_x_y;
                if (det_a != 0) Console.WriteLine($"Det a este: {det_a}");

                det_m = n * suma_x_patrat - Math.Pow(suma_xi, 2);
                if (det_m != 0) Console.WriteLine($"Det M este: {det_m}");

                a_est = det_a / det_m;
                if (a_est != 0) Console.WriteLine($"A est este: {a_est}");

                det_b = n * suma_x_y - suma_xi * suma_yi;
                if (det_b != 0) Console.WriteLine($"Det b este: {det_b}");

                b_est = det_b / det_m;
                if (b_est != 0) Console.WriteLine($"B est este: {b_est}");

                ta = Math.Abs(a_est / sa);
                if (ta != 0) Console.WriteLine($"Ta este: {ta}");

                tb = Math.Abs(b_est / sb);
                //Debug.WriteLine($"b est: {b_est} sb: {sb}");
                if (tb != 0) Console.WriteLine($"Tb este: {tb}");

                if (ta != 0 || tb != 0)
                {
                    if (ta < t_critic && tb < t_critic)
                    {
                        Console.WriteLine("ipoteza nula se accepta, adică se afirma cu probabilitatea p că cei doi coeficienți sunt în realitate nuli");
                    }
                    if (ta > t_critic && tb > t_critic)
                    {
                        Console.WriteLine("ipoteza nula se respinge, adică se admite cu probabilitatea p că cei doi coeficienți sunt în realitate nenuli");
                    }
                    if (ta > t_critic && tb < t_critic)
                    {
                        Console.WriteLine($"Putem afirma cu o probabilitate de x% ca tb nu este nenul iar ta este un estimator corect");
                    }
                    if (ta < t_critic && tb > t_critic)
                    {
                        Console.WriteLine($"Putem afirma cu o probabilitate de x% ca ta nu este nenul iar tb este un estimator corect");
                    }
                }

                

                coef_corelatie = (n * suma_x_y - suma_xi * suma_yi) / Math.Sqrt((n * suma_x_patrat - Math.Pow(suma_xi, 2)) * (n * suma_y_patrat - Math.Pow(suma_yi, 2)));
                if (coef_corelatie != 0) Console.WriteLine($"Coeficientul de corelatie(r) este: {coef_corelatie}");

                coef_det = 1 - suma_y_minus_y_est_patrat / suma_y_minus_y_med_patrat;
                if (coef_det != 0)
                {
                    Console.WriteLine($"Coeficientul de determinatie R^2 este: {coef_det}");
                    Console.WriteLine("Daca R^2 tinde spre 1 inseamna ca exista o corelatie puternica");
                    Console.WriteLine("Daca R^2 tinde spre 0 inseamna ca corelatia este slaba sau inexistenta");

                }

                







            }

            void PLSHPartial()
            {
                Console.WriteLine("0. Se cunoaste valoarea lui n? Pune 0 daca nu.");
                n = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("1. Se cunoaste media lui Y? Pune valoarea 0 daca nu.");
                media_yi = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("2. Se cunoaste suma lui Z(1/X)? Pune valoarea 0 daca nu.");
                suma_zi = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("3. Se cunoaste suma lui Y? Pune valoarea 0 daca nu.");
                suma_yi = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("4. Se cunoaste suma lui Y^2? Pune valoarea 0 daca nu.");
                suma_y_patrat = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("5. Se cunoaste suma lui Z^2? Pune valoarea 0 daca nu.");
                suma_z_patrat = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("6. Se cunoaste suma produsului Z * Y? Pune valoarea 0 daca nu.");
                suma_z_y = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("7. Se cunoaste suma lui Y estimat? Pune valoarea 0 daca nu.");
                suma_y_estimat = Convert.ToDouble(Console.ReadLine());               
                Console.WriteLine("8. Se cunoastea suma lui (Y-Y estimat)^2? Pune valoarea 0 daca nu.");
                suma_y_minus_y_est_patrat = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("9. Se cunoaste suma lui (Y-Y mediu)^2 ? Pune valoarea 0 daca nu.");
                suma_y_minus_y_med_patrat = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("10. Se cunoaste suma lui (Y estimat - Y mediu) ^2 ? Pune valoarea 0 daca nu.");
                suma_y_est_minus_y_med_patrat = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("11. Se cunoaste coeficientul de corelatie (r)? Pune valoarea 0 daca nu.");
                coef_corelatie = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("12. Se cunoaste S epsilon? Pune valoarea 0 daca nu.");
                s_epsilon = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("13. Se cunoastea valoarea lui s a? Pune valoarea 0 daca nu.");
                sa = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("14. Se cunoaste valoarea lui s b? Pune valoarea 0 daca nu.");
                sb = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("15. Se cunoaste valoarea lui T a ? Pune valoarea 0 daca nu.");
                ta = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("16. Se cunoaste valoarea lui T b ? Pune valoarea 0 daca nu.");
                tb = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("17. Se cunoaste t-ul critic? Pune valoarea 0 daca nu.");
                t_critic = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("18. Se cunoaste coeficientul de determinatie (R^2)? Pune valoarea 0 daca nu.");
                coef_det = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("19. Se cunoaste determinatul lui a? Pune valoarea 0 daca nu.");
                det_a = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("20. Se cunoaste determinatul lui M? Pune valoarea 0 daca nu.");
                det_m = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("21. Se cunoaste determinatul lui b? Pune valoarea 0 daca nu.");
                det_b = Convert.ToDouble(Console.ReadLine());

                
                if (coef_corelatie != 0) Console.WriteLine($"Coeficientul de corelatie(r) este: {coef_corelatie}");



                s_epsilon = Math.Sqrt((suma_y_minus_y_est_patrat) / (n - 2));
                if (s_epsilon != 0) Console.WriteLine($"S epsilon este: {s_epsilon}");

                sa = s_epsilon * Math.Sqrt((suma_z_patrat) / (n * suma_z_patrat - Math.Pow(suma_zi, 2)));
                if (sa != 0) Console.WriteLine($"Sa este: {sa}");

                sb = s_epsilon * Math.Sqrt((n) / (n * suma_z_patrat - Math.Pow(suma_zi, 2)));
                if (sb != 0) Console.WriteLine($"Sb este: {sb}");

                det_a = suma_z_patrat * suma_yi - suma_zi * suma_z_y;
                if (det_a != 0) Console.WriteLine($"Det a este: {det_a}");

                det_m = n * suma_z_patrat - Math.Pow(suma_zi, 2);
                if (det_m != 0) Console.WriteLine($"Det M este: {det_m}");

                a_est = det_a / det_m;
                if (a_est != 0) Console.WriteLine($"A est este: {a_est}");

                det_b = n * suma_z_y - suma_zi * suma_yi;
                if (det_b != 0) Console.WriteLine($"Det b este: {det_b}");

                b_est = det_b / det_m;
                if (b_est != 0) Console.WriteLine($"B est este: {b_est}");

                ta = Math.Abs(a_est / sa);
                if (ta != 0) Console.WriteLine($"Ta este: {ta}");

                tb = Math.Abs(b_est / sb);
                //Debug.WriteLine($"b est: {b_est} sb: {sb}");
                if (tb != 0) Console.WriteLine($"Tb este: {tb}");

                if (ta != 0 || tb != 0)
                {
                    if (ta < t_critic && tb < t_critic)
                    {
                        Console.WriteLine("ipoteza nula se accepta, adică se afirma cu probabilitatea p că cei doi coeficienți sunt în realitate nuli");
                    }
                    if (ta > t_critic && tb > t_critic)
                    {
                        Console.WriteLine("ipoteza nula se respinge, adică se admite cu probabilitatea p că cei doi coeficienți sunt în realitate nenuli");
                    }
                    if (ta > t_critic && tb < t_critic)
                    {
                        Console.WriteLine($"Putem afirma cu o probabilitate de x% ca tb nu este nenul iar ta este un estimator corect");
                    }
                    if (ta < t_critic && tb > t_critic)
                    {
                        Console.WriteLine($"Putem afirma cu o probabilitate de x% ca ta nu este nenul iar tb este un estimator corect");
                    }
                }



                coef_corelatie = (n * suma_z_y - suma_zi * suma_yi) / Math.Sqrt((n * suma_z_patrat - Math.Pow(suma_zi, 2)) * (n * suma_y_patrat - Math.Pow(suma_yi, 2)));
                if (coef_corelatie != 0) Console.WriteLine($"Coeficientul de corelatie(r) este: {coef_corelatie}");

                coef_det = 1 - suma_y_minus_y_est_patrat / suma_y_minus_y_med_patrat;
                if (coef_det != 0)
                {
                    Console.WriteLine($"Coeficientul de determinatie R^2 este: {coef_det}");
                    Console.WriteLine("Daca R^2 tinde spre 1 inseamna ca exista o corelatie puternica");
                    Console.WriteLine("Daca R^2 tinde spre 0 inseamna ca corelatia este slaba sau inexistenta");

                }

            }

            void FunctieTimp()
            {
                Console.WriteLine("Cate perioade are exercitiul (n) ?");
                n = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Introduceti t-ul final pentru care se calculeaza Y: ");
                t_final = Convert.ToDouble(Console.ReadLine());

                for (int i = 0; i < n; i++) 
                {
                    Console.WriteLine($"Introduceti Y({i}): ");
                    yi.Add(Convert.ToDouble(Console.ReadLine()));          
                }

                for (int i = 0;i < n; i++) 
                {
                    ti.Add(i + 1);
                    //Debug.WriteLine($"T({i}): {ti[i]}");
                   //Console.WriteLine($"T{i}: {ti[i]}");
                    suma_t_patrat += Math.Pow(ti[i], 2);
                    suma_t_y += ti[i] * yi[i];
                    suma_yi += yi[i];
                    suma_ti += ti[i];
                }

                a_est = (suma_t_patrat * suma_yi - suma_ti * suma_t_y) / (n * suma_t_patrat - Math.Pow(suma_ti,2));
                Console.WriteLine($"A est: {a_est}");
                //Debug.WriteLine($"Numitor a: ({suma_t_patrat} * {suma_yi} - {suma_ti} * {suma_t_y}) = {(suma_t_patrat * suma_yi - suma_ti * suma_t_y)}");
                //Debug.WriteLine($"Numarator a: ({n} * {suma_t_patrat} - {Math.Pow(suma_ti,2)} = {(n * suma_t_patrat - Math.Pow(suma_ti, 2))}");
                b_est = (n * suma_t_y - suma_ti * suma_yi) / (n * suma_t_patrat - Math.Pow(suma_ti,2));
                Console.WriteLine($"B est: {b_est}");
                //Debug.WriteLine($"Numitor b: ({n} * {suma_t_y} - {suma_ti} * {suma_yi} = {(n * suma_t_y - suma_ti * suma_yi)})");
                //Debug.WriteLine($"Numarator b: ({n} * {suma_t_patrat} - {Math.Pow(suma_ti,2)} = {(n * suma_t_patrat - Math.Pow(suma_ti, 2))}");

                Console.WriteLine($"Y = {a_est} + {b_est} * {t_final} + epsilon = {a_est + b_est * t_final}");

            }

            void Autoregresiv()
            {
                Console.WriteLine("Cate valori are exercitiul ? ");
                n = Convert.ToInt32(Console.ReadLine());

                for (int i = 0; i < n; i++)
                {
                    Console.WriteLine($"Introduceti numarul {i}:");
                    yi.Add(Convert.ToDouble(Console.ReadLine()));
                }

                for (int i=0; i< n-1; i++)
                {
                    yt.Add(yi[i+1]);
                    yt_minus_unu.Add(yi[i]);
                    yt_minus_unu_patrat.Add(Math.Pow(yt_minus_unu[i],2));
                    yt_minus_unu_yt.Add(yt_minus_unu[i] * yt[i]);
                    suma_yt += yt[i];
                    suma_yt_minus_unu += yt_minus_unu[i];
                    suma_yt_minus_unu_patrat += yt_minus_unu_patrat[i];
                    suma_yt_minus_unu_yt += yt_minus_unu_yt[i];
                    Debug.WriteLine(yt[i]);
                }
                a_est = (suma_yt_minus_unu_patrat * suma_yt - suma_yt_minus_unu * suma_yt_minus_unu_yt) / ((n - 1) * suma_yt_minus_unu_patrat - Math.Pow(suma_yt_minus_unu,2));
                Console.WriteLine($"A est: {a_est}");
                b_est = ((n - 1) * suma_yt_minus_unu_yt - suma_yt * suma_yt_minus_unu) / ((n - 1) * suma_yt_minus_unu_patrat - Math.Pow(suma_yt_minus_unu,2));
                Console.WriteLine($"B est: {b_est}");
                Console.WriteLine($"Yt = {a_est} + {b_est} * {yt[yt.Count-1]} = {a_est + b_est * yt[yt.Count-1]}");

                //Console.WriteLine($"yt: {suma_yt}");
                //Console.WriteLine($"yt-1: {suma_yt_minus_unu}");
                //Console.WriteLine($"yt-1^2: {suma_yt_minus_unu_patrat}");
                //Console.WriteLine($"yt-1*yt: {suma_yt_minus_unu_yt}");


            }

            void AutoregresivPartial()
            {
                Console.WriteLine("1. Introduceti valorea lui n;");
                n = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("2. Introduceti suma lui Y(t). Pune 0 daca nu se cunoaste valoarea.");
                suma_yt = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("3. Introduceti suma lui Y(t-1). Pune 0 daca nu se cunoaste valoarea.");
                suma_yt_minus_unu = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("4. Introduceti suma lui Y^2(t-1). Pune 0 daca nu se cunoaste valoarea.");
                suma_yt_minus_unu_patrat = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("5. Introduceti suma lui Y(t-1) * Y(t). Pune 0 daca nu se cunoaste valoarea.");
                suma_yt_minus_unu_yt = Convert.ToDouble(Console.ReadLine());

                a_est = (suma_yt_minus_unu_patrat * suma_yt - suma_yt_minus_unu * suma_yt_minus_unu_yt) / (n * suma_yt_minus_unu_patrat - Math.Pow(suma_yt_minus_unu, 2));
                Console.WriteLine($"A est: {a_est}");
                b_est = (n * suma_yt_minus_unu_yt - suma_yt * suma_yt_minus_unu) / (n * suma_yt_minus_unu_patrat - Math.Pow(suma_yt_minus_unu, 2));
                Console.WriteLine($"B est: {b_est}");
                //Console.WriteLine($"Yt = {a_est} + {b_est} * {n} = {a_est + b_est * n}");

            }

            void FunctieTimpPartial()
            {
                Console.WriteLine("1. Introduceti valoarea lui n");
                n = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("2. Introduceti suma lui Y.");
                suma_yi = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("3. Introduceti suma lui t.");
                suma_ti = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("4. Introduceti suma lui t^2.");
                suma_t_patrat = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("5. Introduceti suma lui t * y.");
                suma_t_y = Convert.ToDouble(Console.ReadLine());

                a_est = (suma_t_patrat * suma_yi - suma_ti * suma_t_y) / (n * suma_t_patrat - Math.Pow(suma_ti, 2));
                Console.WriteLine($"A est: {a_est}");
                b_est = (n * suma_t_y - suma_ti * suma_yi) / (n * suma_t_patrat - Math.Pow(suma_ti, 2));
                Console.WriteLine($"B est: {b_est}");
            }



            Console.WriteLine("Pentru Progresia liniara simpla cu date complete apasa 1 ");
            Console.WriteLine("Pentru Model hiperbolic apasa 2 ");
            Console.WriteLine("Pentru Progresia liniara simpla cu date partiale apasa 3.");
            Console.WriteLine("Pentru modelul hiperbolic cu date partiale apasa 4.");
            Console.WriteLine("Pentru functia de timp apasa 5.");
            Console.WriteLine("Pentru functia de timp cu date partiale apasa 6.");
            Console.WriteLine("Pentru model autoregresiv apasa 7.");
            Console.WriteLine("Pentru model autoregresiv cu date partiale apasa 8.");
            raspuns_cautare = Convert.ToString(Console.ReadLine());

            if (raspuns_cautare == "1")
            {
                PLS();        
            }
            if (raspuns_cautare == "2")
            {
                PLSH();
            }

            if (raspuns_cautare == "3")
            {
                PLSPartial();
            }

            if(raspuns_cautare == "4")
            {
                PLSHPartial();
            }

            if (raspuns_cautare == "5")
            {
                FunctieTimp();
            }

            if (raspuns_cautare =="6")
            {
                FunctieTimpPartial();
            }

            if (raspuns_cautare=="7")
            {
                Autoregresiv();
            }

            if (raspuns_cautare == "8")
            {
                AutoregresivPartial(); 
            }


            
        }
        
    }
}
