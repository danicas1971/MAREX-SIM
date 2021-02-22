// -------------- MAREX-SIM v1.0: A simulator for MAREX Architecture ---------------

// Daniel Cascado-Caballero, Fernando Diaz-del-Rio, Daniel Cagigas-Muñiz,
// Antonio Rios-Navarro, Jose-Luis Guisado-Lizar,
//   Dept. of Computer Architecture and Technology,
//   Universidad de Sevilla, Seville, Spain

//   Ignacio Pérez-Hurtado, Agustín Riscos-Nuñez
//     Research Group on Natural Computing,
//   Dept. of Computer Science and Artificial Intelligence
//     Universidad de Sevilla, Seville, Spain
//  --------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MaquinaFlujo2
{
    public class C_EL : IComparable<C_EL>
    {
        public const string TIPO_ALFA = "ALF";
        public const string TIPO_OMEGA = "OME";
        public const string TIPO_NULO = "NUL";
        public const string TIPO_DELTA = "-d-";

        
        public string til;
        public int c_ac, c_en;
        public char et; 
        public char id; 
        
        public int al; 


        public int col; 
        public int ral; 


        public C_EL()
        {
            til = TIPO_NULO;
            c_ac = 0;
            c_en = 0;
        }
        public C_EL(string tere, int cdx = 0, int ecld = 0, int ider = 0)
        {
            Init(tere, cdx, ecld, ider);
        }
        public void Init(string a_tipo, int a_cal = 0, int a_el = 0, int a_ir = 0)
        {
            til = a_tipo;
            c_ac = a_cal;
            c_en = a_el;
            al = a_ir;
        }

        public void f_cde(C_EL origen)
        {
            til = origen.til;
            c_ac = origen.c_ac;
            c_en = origen.c_en;
            al = origen.al;
            ral = origen.ral;
        }


        public string f_ump(bool nulidf) {
            string cadena = "";
            if (til != TIPO_NULO)
                cadena += til.ToString() + "*" + c_ac.ToString() + "/" + c_en.ToString();
            else
                cadena += til + "  ";

            return cadena;
        }

        public int CompareTo(C_EL comparado)
        {
            if (comparado.til == null)
            {
                if (til == null)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                if (til == null)
                {
                    return 1;
                }
                else
                {
                    int retval = comparado.til.Length.CompareTo(til.Length);

                    if (retval != 0)
                    {
                        return retval;
                    }
                    else
                    {
                        return -comparado.til.CompareTo(til);
                    }
                }
            }
        }


    }

    public class C_2EL
    {
        const int N_DER = 3;
        const int N_IZQ = 3;
        public C_EL[] P_una = new C_EL[N_IZQ];
        public C_EL[] P_dos = new C_EL[N_IZQ];

        public int[] achj = new int[N_IZQ];
        public int coeli;
        public int puix;

        
        public const int Rm = 1;
        public const int RM = -1;
        private int ContE, cdd; 

        public bool aix;



        public C_2EL()
        {
            int idx_item;
            puix = 0;
            aix = true;
            coeli = 0;
            for (idx_item = 0; idx_item < N_IZQ; idx_item++)
            {
                P_dos[idx_item] = new C_EL(C_EL.TIPO_NULO);
                achj[idx_item] = 0;
            }
            for (idx_item = 0; idx_item < N_DER; idx_item++)
            {
                P_una[idx_item] = new C_EL(C_EL.TIPO_NULO);
            }

        }

        public void Init(string cas, int[] pol, int msx = RM)
        {
            int idx_item = 0;
            bool p_iz = true;
            int pos,pos_mult;
            string item;

            cdd = msx;
            do
            {
                pos = cas.IndexOf(' ');
                if (pos > 0)
                    item = cas.Substring(0, pos);
                else
                    item = cas;

                if (item != "->")
                {
                    pos_mult = item.IndexOf('*');
                    if (p_iz == true)
                    {   
                        P_dos[idx_item].til = item.Substring(0, pos_mult); 

                        item = item.Remove(0,pos_mult+1);
                        P_dos[idx_item].c_ac = Convert.ToInt32(item);
                        P_dos[idx_item].col = pol[idx_item];   

                    }
                    else
                    {
                        P_una[idx_item].til = item.Substring(0, pos_mult); 
                        
                        item = item.Remove(0, pos_mult + 1);
                        P_una[idx_item].c_ac = Convert.ToInt32(item);
                    }
                    idx_item++;
                }
                else
                {
                    p_iz = false;
                    coeli = idx_item;
                    idx_item = 0;

                }
                if (pos > 0)
                    cas = cas.Remove(0, pos + 1);
                else
                    cas = "";

            } while (cas.Length > 0);


        }

        public bool f_erela(C_EL ccc_el, int alex, bool miyo)
        {
            int i, cont_conseguidos = 0;
            bool resultado = false;

            if (ccc_el.til != C_EL.TIPO_OMEGA && ccc_el.til != C_EL.TIPO_NULO && ccc_el.til != C_EL.TIPO_ALFA)
            {

                
                for (i = 0; i < N_IZQ; i++)
                {
                    if (P_dos[i].til == ccc_el.til)
                    {
                        if (puix != 0)
                        {
                
                            ccc_el.c_ac += P_dos[i].c_en;
                            P_dos[i].c_en = 0;
                        }
                        else
                        {
                
                            if (ccc_el.c_ac > 0 && alex <= P_dos[i].col && aix && (ContE > 0 || ContE == RM))
                
                            {
                
                                if (miyo)
                                {
                                    P_dos[i].c_en += ccc_el.c_ac; 
                                    ccc_el.c_ac = 0;
                                }
                                else
                                {
                                    int temporal;
                                    temporal = P_dos[i].c_ac - P_dos[i].c_en; 
                                    if (P_dos[i].c_en < P_dos[i].c_ac && temporal <= ccc_el.c_ac)
                                    {
                                        
                                        P_dos[i].c_en += temporal;
                                        ccc_el.c_ac -= temporal;
                                    }
                                }

                            }
                        }
                    }
                }



            }

            bool ha_conseguido_algo = false;
            cont_conseguidos = 0;
            for (i = 0; i < N_IZQ; i++)
            {
                if (P_dos[i].c_en > 0 && P_dos[i].til != C_EL.TIPO_NULO)
                    ha_conseguido_algo = true; 

                if (P_dos[i].c_en >= P_dos[i].c_ac && P_dos[i].til != C_EL.TIPO_NULO)
                    cont_conseguidos++; 
            }

            if ((cont_conseguidos > 0 || ha_conseguido_algo) && cont_conseguidos < coeli && ccc_el.til == C_EL.TIPO_OMEGA)
                puix = 1; 




            if (ha_conseguido_algo && puix > 0 && ccc_el.til == C_EL.TIPO_OMEGA)
                ccc_el.c_ac += 1;

            if (ccc_el.til == C_EL.TIPO_ALFA)
            {
                ContE = cdd; 
                puix = 0; 
            }

            if (cont_conseguidos == coeli)
            {

                for (i = 0; i < N_DER; i++)
                {
                    P_una[i].c_en += P_una[i].c_ac;
                }

                for (i = 0; i < N_IZQ; i++)
                {
                    if (P_dos[i].til != C_EL.TIPO_NULO)
                        P_dos[i].c_en -= P_dos[i].c_ac;
                }

                if (ContE != -1 && ContE > 0)
                {
                    ContE--;

                    if (ContE == 0)
                        puix = 1;
                }
                resultado = true;
            }


            for (i = 0; i < N_DER; i++)
            {
                if (P_una[i].til == ccc_el.til)
                {
                    ccc_el.c_en += P_una[i].c_en;
                    P_una[i].c_en = 0;
                }

                if (P_una[i].c_en > 0 && ccc_el.til == C_EL.TIPO_OMEGA)
                    ccc_el.c_ac++;
            }
            return resultado;
        }

        public string f_impla()
        {
            int j;
            string cadena_der, cadena_iz, cadena_purga, cadena_activacion = "";


            cadena_der = cadena_iz = "";
            for (j = 0; j < N_IZQ; j++)
            {
                if (P_dos[j].til != C_EL.TIPO_NULO)
                    cadena_iz += P_dos[j].f_ump(false) + " ";
            }

            for (j = 0; j < N_DER; j++)
            {
                if (P_una[j].til != C_EL.TIPO_NULO)
                    cadena_der += P_una[j].f_ump(false) + " ";
            }
            if (puix == 0)
                cadena_purga = " ";
            else
                cadena_purga = "P";

            if (aix)
                cadena_activacion = "*";
            else
                cadena_activacion = "o";

            return "[" + cadena_activacion + cadena_purga + "] " + cadena_iz + " $\\rightarrow$ " + cadena_der;


        }

        public string f_imp()
        {
            int j;
            string cadena_der, cadena_iz, cadena_purga, cadena_activacion = "";


            cadena_der = cadena_iz = "";
            for (j = 0; j < N_IZQ; j++)
            {
                if (P_dos[j].til != C_EL.TIPO_NULO)
                    cadena_iz += P_dos[j].f_ump(false) + " ";
            }

            for (j = 0; j < N_DER; j++)
            {
                if (P_una[j].til != C_EL.TIPO_NULO)
                    cadena_der += P_una[j].f_ump(false) + " ";
            }
            if (puix == 0)
                cadena_purga = " ";
            else
                cadena_purga = "P";

            if (aix)
                cadena_activacion = "*";
            else
                cadena_activacion = "o";

            return "[" + cadena_activacion + cadena_purga + "] " + cadena_iz + " -> " + cadena_der;


        }
    }


    class C_EEL
    {
        private int N_CEL { get { return rex.Count; } }
        public int N_EEL { get { return so.Count; } }

        public int Np = 0; 
        private Random ga;
        private Random ge;
        private int ib = 0;
        private bool ese;
        private bool p_co = true;
        private bool p_ro = false;
        public int n_ej = 0;
        public int Nps = 0;


        private List<C_2EL> rex = new List<C_2EL>();

        public List<C_EL> so = new List<C_EL>(); 
        private List<C_EL> bur = new List<C_EL>();

        public bool fpr;
       


        public C_EEL(int sa,bool re)
        {
       
            ge = new Random(sa);
            ga = ge;
            p_ro = re;
        }

        public void Init(bool re)
        {
            int i;
            int rs = ga.Next(0, N_EEL - 1);
            fpr = false;
            p_co = true;
            ese = re;
            f_ae(C_EL.TIPO_OMEGA, 0, 0, false);

            if (p_ro)
                for (i = 0; i < rs; i++) f_rsop();
        }
        public void f_ar(string cadena, int[] px, int mx = C_2EL.RM)
        {
            C_2EL rel = new C_2EL();
            rel.Init(cadena, px, mx);
            rex.Add(rel);
            bur.Add(new C_EL(C_EL.TIPO_NULO));
        }

        public void f_ae(string ti, int ca = 0, int encl = 0, bool al = true)
        {
            if (!al)
            {
                
                so.Add(new C_EL(ti, ca, encl));
                so[so.Count - 1].al = so.Count - 1;
            } else
            {
                int valor = ge.Next(1, 100);
                if (valor < 50)
                {
                
                    so.Add(new C_EL(ti, ca, encl));
                    so[so.Count - 1].al = so.Count - 1;
                }
                else
                {
                
                    so.Insert(0, new C_EL(ti, ca, encl));
                
                    for (int i = 0; i < so.Count; i++)
                    {
                        so[i].al = i;
                    }
                }
            }

        }

        private void f_IRLA()
        {
            int i;
            string cadena;

            for (i = 0; i < N_CEL; i++)
            {
                cadena = bur[i].f_ump(true);
                Console.WriteLine("    \\STATE (" + cadena.PadRight(10) + ") " + rex[i].f_impla());

            }

        }



        private void f_ir() {
            int i;
            string cadena;

            for (i = 0; i < N_CEL; i++) {
                cadena = bur[i].f_ump(true);
                Console.WriteLine("║ [ " + cadena.PadRight(10) + "] " + rex[i].f_imp());

            }

        }

        public void f_isd()
        {
            string cadena = "";
            int i;

            for (i = 0; i < N_EEL; i++)
            {
                

                cadena += so[i].f_ump(true) + " ";
            }
            Console.Write(cadena + " ");

        }

        public void f_isod()
        {
            string cadena = "";
            int i;

            so.Sort();

            for (i = 0; i < N_EEL; i++)
            {
                
                if (so[i].c_ac > 0 || so[i].c_en > 0)
                    cadena += so[i].f_ump(true) + " ";
            }
            Console.Write(cadena + " ");

        }

        public void f_isodl()
        {
            int i;
            string cadena = "";

            for (i = 0; i < N_EEL; i++)
            {

                if (so[i].til != C_EL.TIPO_OMEGA)
                {
                    if (i == 0)
                        cadena += so[i].f_ump(true);
                    else
                        cadena += " " + so[i].f_ump(true);
                }

            }
            Console.Write(cadena);
        }

        public void f_isops()
        {
            int i;
            string cadena = "║";


            for (i = 0; i < N_EEL; i++)
            {

                if (so[i].til != C_EL.TIPO_OMEGA)
                {
                    cadena += " " + so[i].f_ump(true).PadRight(10);
                }

                if (cadena.Length >= 60 || i == N_EEL - 1) 
                {
                    Console.WriteLine(cadena.PadRight(74) + "║");
                    cadena = "║";
                }
            }
            Console.WriteLine("╚══════════════════════" + " Cycle: " + Np.ToString().PadRight(17) + "══════════════════════════╝");

        }

        private void f_ibu() {
            Console.WriteLine("╠══════════════════════ GLOBAL OBJECT CONTAINER ══════════════════════════╗");

        }
        private void f_dm()
        {
            int i;
            for (i = 0; i < N_CEL; i++)
                rex[i].aix = false;
        }


        private void f_rd(bool imprimir)
        {
            int i;
            for (i = 0; i < N_EEL; i++)
            {
                if (so[i].til == C_EL.TIPO_DELTA && so[i].c_ac > 0)
                {
                    f_dm();
                    so[i].c_ac = 0;
                    if (imprimir)
                    {
                        Console.WriteLine("══════════════════════════  Disolution reached!! ═══════════════════════════════\n");

                    }
                }
            }
        }
        private void f_ce(bool imp)
        {
            int i;
            int contenco = 0;

            for (i = 0; i < N_EEL; i++)
            {
                contenco += so[i].c_en;
                so[i].c_ac += so[i].c_en;
                so[i].c_en = 0;
            }
            if (imp) Console.WriteLine("═════════════════════════ Computation Step reached!! ═══════════════════════════\n");
            f_rd(imp);
            if (contenco == 0) fpr = true;

        }
        private Tuple<string, bool> f_boel(bool m_alf, bool act_cop = true)
        {
            int i;
            string tiul;
            bool vaul = false;
            int indul;
            int aled;
            Tuple<string, bool> res;

            Np++; 

            tiul = bur[N_CEL - 1].til;

            if (tiul != C_EL.TIPO_NULO && tiul != C_EL.TIPO_OMEGA && tiul != C_EL.TIPO_ALFA) {
                indul = bur[N_CEL - 1].al;

                if (so[indul].c_ac != bur[N_CEL - 1].c_ac || so[indul].c_en != bur[N_CEL - 1].c_en)
                    vaul = true;

                if (act_cop) so[indul].f_cde(bur[N_CEL - 1]);

            }
            if (tiul == C_EL.TIPO_OMEGA && bur[N_CEL - 1].c_ac > 0)
                vaul = true;

            bur.RemoveAt(N_CEL - 1);
            
            if (m_alf)
            {
            
                ib = 0;
                bur.Insert(0, new C_EL(C_EL.TIPO_ALFA));
            }
            else
            {

            
                if (ib < N_EEL)
                {
            
                    bur.Insert(0, new C_EL());
                    bur[0].f_cde(so[ib]);
            
                    ib++;  
                }
                else
                {
                    bur.Insert(0, new C_EL(C_EL.TIPO_NULO));
                    
                    if (tiul == C_EL.TIPO_OMEGA)
                        ib = 0; 
                }
            }
            

            
            for (i = 0; i < N_CEL; i++) {
                aled = ga.Next(1, 100); 
                bur[i].ral = aled;
                if (rex[i].f_erela(bur[i], aled, ese)) n_ej++;
            }
            res = new Tuple<string, bool>(tiul, vaul);
            return res;
        }


        public void f_impf(bool enlat = false)
        {
            if (enlat)
            {
                Console.Write("\\begin{systemstatus} \\caption{Cycle  " + Np.ToString() + ". GOC= ");
                f_isodl(); Console.WriteLine("}");
                Console.WriteLine("\\begin{algorithmic}");
                f_IRLA();
                Console.WriteLine("\\end{algorithmic} \\end{systemstatus}");
            }
            else
            {
                f_ir();
                f_ibu();
                f_isops();
            }
        }
        public void f_ex(bool paspas, bool i_lat = false)
        {

            string caracter = "";
            Tuple<string, bool> r_boe;
            bool e_var_alg = false;


            fpr = false;
            r_boe = f_boel(true); 
            if (paspas)
            {
                f_impf(i_lat);
                Console.WriteLine();
            }
            do
            {
                if (r_boe.Item1 == C_EL.TIPO_ALFA || r_boe.Item1 == C_EL.TIPO_OMEGA) {
                    e_var_alg = false;
                }
                r_boe = f_boel(false, p_co);
                
                if (!p_co && r_boe.Item1 == C_EL.TIPO_OMEGA) p_co = true;
                e_var_alg = r_boe.Item2 || e_var_alg;
                if (paspas) {
                    f_impf(i_lat); caracter = Console.ReadLine();
                }
                
                if (p_ro && r_boe.Item1==C_EL.TIPO_OMEGA)
                    f_rsop(Np % (3) == 0 ? true : false);

            } while (r_boe.Item1 != C_EL.TIPO_OMEGA || e_var_alg); 

            
            f_ce(paspas);

            Nps++;

        }
        
        void f_rsop(bool ivex = false)
        {

            C_EL elex;

            elex = new C_EL();

        
            elex.f_cde(so[so.Count - 2]);
            so.RemoveAt(so.Count - 2);
            so.Insert(0, elex);

            if (N_EEL > 2 && ivex)
                so.Reverse(1, (N_EEL - 1) / 2);

        
            for (int i = 0; i < so.Count - 1; i++)
                so[i].al = i;
        }


    }

    class Program
    {
        static C_EEL mp;

        static void mref()
        {
            mp.f_ar("_A_*1 _B_*1 -> _C_*1", new int[] { 100, 100 });
            mp.f_ar("_A_*1 _C_*1 -> _B_*1 _D_*1", new int[] { 100, 100 });
            mp.f_ar("_D_*1 -> -d-*1", new int[] { 100 });
            mp.f_ae("_A_", 1, 0, false);
            mp.f_ae("_B_", 1, 0, false);
            mp.f_ae("_C_", 1, 0, false);
            mp.f_ae("_D_", 0, 0, false);
            mp.f_ae(C_EL.TIPO_DELTA, 0, 0, false);
        }

        static bool csmr(C_EEL m)
        {
            return m.fpr;
        }

        static void mfib(int n)
        {
            int i;
            string car, care;

            for (i = 1; i <= n; i++)
            {
                // r01  [a{i,1}]'1 --> [a{i},a{i+2,0}]'1 : 1<=i<=n;
                car = string.Format("a{0}1*1 -> a{0}_*1 a{1}0*1", i, i + 2);
                mp.f_ar(car, new int[] { 100 });

                // r02  [a{i+1,0}]'1 --> [a{i+1,1},a{i+2,0}]'1 : 1<=i<=n;
                car = string.Format("a{0}0*1 -> a{0}1*1 a{1}0*1", i + 1, i + 2);
                mp.f_ar(car, new int[] { 100 });
            }

            // Rules r03 and r04 are for cleaning 
            // r03 [a{n+1,1}]'1 --> []'1;
            car = string.Format("a{0}1*1 -> NUL*0", n + 1);
            mp.f_ar(car, new int[] { 100 });
            // r04  [a{n+2,0}]'1 --> []'1;
            car = string.Format("a{0}0*1 -> NUL*0", n + 2);
            mp.f_ar(car, new int[] { 100 });

            for (i = 1; i <= n; i++)
            {

                care = string.Format("a{0}1", i);
                if (i == 1)
                    mp.f_ae(care, 1, 0);
                else
                    mp.f_ae(care, 0, 0);
                care = string.Format("a{0}_", i);
                mp.f_ae(care, 0, 0);
                care = string.Format("a{0}0", i + 1);
                if (i == 1)
                    mp.f_ae(care, 1, 0);
                else
                    mp.f_ae(care, 0, 0);
            }
            care = string.Format("a{0}1", n + 1);
            mp.f_ae(care, 1, 0);
            care = string.Format("a{0}0", n + 2);
            mp.f_ae(care, 1, 0);
        }

        
        static bool csfi(C_EEL m, int n)
        {
            int[] serie = new int[] { 0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89 };
            int i;
            bool resultado = false;
            string tbu = string.Format("a{0}_", n);
            for (i = 0; i < m.so.Count; i++) 
            {

                if (m.so[i].til == tbu)
                {
                    if (m.so[i].c_ac == serie[n]) resultado = true;
                }
            }


            return resultado;

        }

        static bool cspi(C_EEL m)
        {
            return m.fpr;
        }

        static void tpip(int n){
            int i;
            string cao, car;
            for (i=0;i<n; i++)
            {
                car = string.Format("a{0}*1 -> b{0}*1", i);
                mp.f_ar(car, new int[] { 100 });
                cao = string.Format("a{0}", i);
                mp.f_ae(cao, n, 0);
                cao = string.Format("b{0}", i);
                mp.f_ae(cao, 0, 0);
            }


        }


        
        static void mrgfo(int n)
        {
            int nel = (n * 2) - 2;
            int r_i_i = (int) (100.0 / nel);
            int[] pai = new int[10];
            int[,] paij=new int[10,10];
            int i, j;
            string cadena;

            for (i = 1; i <= n; i++)
            {
                for (j = i + 1; j <= n; j++)
                {
                    pai[i] = r_i_i;
                    pai[j] = r_i_i;
                    paij[i, j] = 50;
                    cadena = string.Format("a{0}_*1 a{1}_*1 s{0}{1}*1 -> a{0}_*1 a{1}_*1", i, j);
                    mp.f_ar(cadena, new int[] { pai[i], pai[j], paij[i, j] });
                    pai[i] = r_i_i;
                    pai[j] = r_i_i;
                    paij[i, j] = 50;
                    cadena = string.Format("a{0}_*1 a{1}_*1 s{0}{1}*1 -> a{0}_*1 a{1}_*1 e{0}{1}*1", i, j);
                    mp.f_ar(cadena, new int[] { pai[i], pai[j], paij[i, j] });
                }
            }
            for (i=1;i<=n; i++)
            {
                cadena = string.Format("a{0}_", i);
                mp.f_ae(cadena, 1, 0);
            }

            for (i = 1; i <= n; i++)
            {
                for (j = i + 1; j <= n; j++)
                {
                    cadena = string.Format("s{0}{1}", i,j);
                    mp.f_ae(cadena, 1, 0);
                    cadena = string.Format("e{0}{1}", i, j);
                    mp.f_ae(cadena, 0, 0);
                }
            }
        }

        static bool csrg(C_EEL m, int n)
        {
            bool res = true;
            int i,mu;
            string to;
            for (i=0; i<m.so.Count; i++)
            {
                to = m.so[i].til;
                mu = m.so[i].c_ac;
                if (to[0] == 's' && mu > 0)
                    res = false;
            }

            for (i = 0; i < m.so.Count; i++)
            {
                to = m.so[i].til;
                mu = m.so[i].c_ac;
                if (to[0] == 'a' && to[2] == '_' && mu == 0)
                    res = false;

            }


            return res;
        }

        static void impau()
        {
            Console.WriteLine("-------------- MAREX-SIM v1.0: A simulator for MAREX Architecture ---------------" + "\n" );
            Console.WriteLine("  Daniel Cascado-Caballero, Fernando Diaz-del-Rio, Daniel Cagigas-Muñiz," + "\n" + 
                              "  Antonio Rios-Navarro, Jose-Luis Guisado-Lizar," + "\n"+
                              "    Dept. of Computer Architecture and Technology, " + "\n" +
                              "    Universidad de Sevilla, Seville,Spain" + "\n" + "\n" +
                              "  Ignacio Pérez-Hurtado, Agustín Riscos-Nuñez" + "\n" + 
                              "    Research Group on Natural Computing, " + "\n" +
                              "    Dept. of Computer Science and Artificial Intelligence" + "\n"+
                              "    Universidad de Sevilla, Seville, Spain");
            Console.WriteLine("--------------------------------------------------------------------------------" + "\n");
        }

        static void moau()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            string ejecutable= Path.GetFileName(codeBase);

            Console.WriteLine("SINTAX: ");
            Console.WriteLine("  "+ ejecutable +" [m=model] [s=seed] [n=N] [e=egoistic?] [r=object_rotation?] [v=verbosity] \n");

            Console.WriteLine("PARAMETERS:");
            Console.WriteLine("  model: model to simulate. REF by default.\n" +
                              "    REF= Reference model for validating purposes\n" +
                              "    FIBO= Fibonacci model\n" +
                              "    RNDG= Random graph generator model\n" +
                              "    PIPE= Pipeline test model");
            Console.WriteLine("  seed: an integer for initializing the simulator's ramdom number generator. 461 by default.");
            Console.WriteLine("  N: model size (not for REF model). 5 by default");
            Console.WriteLine("  egoistic?: true for using egoistic rules, altruistic if false. True by default.");
            Console.WriteLine("  object_rotation?: true to make GOC rotation after a computation step, if false no rotation is made. False by default.");
            Console.WriteLine("  Verbosity: verbosity level applied in the simulation. 4 by default.");
            Console.WriteLine("   -4: Prints #cycles + #CompSteps + #RuleExecs at the end\n" +
                              "   -3: Prints the number of rule executions at the end\n" +
                              "   -2: Prints the number of computation steps at the end\n" +
                              "   -1: Prints the initial GOC ordering. No evolution is done in the simulator.\n" +
                              "    0: Prints the #Cycles at the end\n" +
                              "    1: Prints the content of the GOC at the end\n" +
                              "    2: Prints the content of the GOC + #cycles at the end\n" +
                              "    3: INTERACTIVE MODEL: Step by step simulation. LaTex printing. Press ENTER to advance\n"+
                              "    4: INTERACTIVE MODEL: Step by step simulation. Normal printing. Press ENTER to advance\n");
            Console.Write("Press ENTER to continue");
            
        }
        static void Main(string[] args)
        {

            string caracter = "s";
            int impre = 4; 
            bool ergo = true;
            int semilla = 461;
            int n = 5,indiarg;
            string mod = "REF";
            bool fin;
            bool mrot = false;

            string idpar,conpar;

            if (args.Length == 0)
            {
        
                impau();
                moau();
                Console.Read();
            }
            else
            {
        
                for (indiarg=0; indiarg<args.Length; indiarg++)
                {
                    try
                    {
                        idpar = args[indiarg].Substring(0, 2);
                        conpar = args[indiarg].Substring(2, args[indiarg].Length - 2);

                        switch (idpar)
                        {
                            case "m=":
                                mod = conpar;
                                break;
                            case "s=":
                                semilla = Convert.ToInt32(conpar);
                                break;
                            case "n=":
                                n = Convert.ToInt32(conpar);
                                break;
                            case "e=":
                                ergo = Convert.ToBoolean(conpar);
                                break;
                            case "r=":
                                mrot = Convert.ToBoolean(conpar);
                                break;
                            case "v=":
                                impre = Convert.ToInt32(conpar);
                                break;
                            default:
                                Console.WriteLine("I don't understand this parameter: " + conpar);
                                break;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error processing parameter: "+args[indiarg] + "\n");
                        Console.WriteLine(e.ToString());
                    }
                }
            }

            if (args.Length != 0)
            {
                mp = new C_EEL(semilla,mrot);
                switch (mod)
                {
                    case "REF":
                        mref();
                        break;
                    case "FIBO":
                        mfib(n);
                        break;
                    case "RNDG":
                        mrgfo(n);
                        break;
                    case "PIPE":
                        tpip(n);
                        break;
                    default:
                        break;
                }
                mp.Init(ergo);
                if (impre > 2)
                {
                    impau();
                    mp.f_impf(impre==3); Console.WriteLine();
                }

        
                if (impre != -1)
                {
                    do
                    {
                        mp.fpr = false;
                        do
                        {
                            mp.f_ex(impre > 2 ? true : false, impre == 3 ? true : false);
        
                        } while (caracter != "n" && mp.fpr == false);
                        switch (mod)
                        {
                            case "REF":
                                fin = csmr(mp);
                                break;
                            case "FIBO":
                                fin = csfi(mp, n);
                                break;
                            case "RNDG":
                                fin = csrg(mp, n);
                                break;
                            case "PIPE":
                                fin = cspi(mp);
                                break;
                            default:
                                fin = true;
                                break;
                        }
                    } while (!fin);
                    if (impre > 2)
                        Console.WriteLine("═════════════════════════      END OF SIMULATION     ═══════════════════════════\n");
                }

        
                switch (impre)
                {
                    case -4:
                        Console.WriteLine(mp.Np.ToString() + " " +
                            mp.Nps.ToString() + " " +
                            mp.n_ej.ToString());
                        break;
                    case -3:
                        Console.WriteLine(mp.n_ej.ToString());
                        break;
                    case -2:
                        Console.WriteLine(mp.Nps.ToString());
                        break;
                    case -1:
                        mp.f_isd();
                        Console.WriteLine();
                        break;
                    case 0:
                        Console.WriteLine(mp.Np.ToString());
                        break;
                    case 1:
                        mp.f_isod();
                        Console.WriteLine();
                        break;
                    case 2:
                        mp.f_isod();
                        Console.WriteLine(": " + mp.Np.ToString());
                        break;
                }

                if (impre > 2)
                {
                    impau();
                    Console.Read();
                }
            }
        }
    }
}
