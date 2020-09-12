using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace APIDemo.Models
{
    public class tree
    {
        /* =================================================
      Variables
      ================================================= */
        public Node root;
        public Node searchEndPos;   // Last node visited by findEntry()

        /* =================================================
           Constructor
           ================================================= */
        public tree() {
            root = null;
        }


        public bool print = false;

        /* ================================================================
           keySearch(k):  find entry containing key k

           Return value:

                 e[i]   if found  (e[i].key == k)
             null   if not found 
                        AND: searchEndPos = node last visited in search
           ================================================================ */
        public Entry keySearch(String k) {
            int lev = 0;
            int i;
            Node curr;

            //     System.out.println("enter keySearch(" + k + ")");
            searchEndPos = root;

            curr = root;
            while (curr != null) {
                searchEndPos = curr;

                if (print) {
                    for (i = 0; i < lev; i++) {
                        Console.Write("     ");
                    }
                    lev++;
                    Console.Write(searchEndPos == null ? "null" : searchEndPos.toString());
                }

                for (i = 0; i < 3; i++) {
                    /* ============================================================
                       It is important to know that a node looks like this:

                       Node:   child[0] entry[0] child[1] entry[1] ...

                   0-node: null     null     null     null ...
                   1-node: impossible !!!
                       2-node: child[0] entry[0] child[1] null ...
                   3-node: child[0] entry[0] child[1] entry[1] child[2] null ...
                   ============================================================= */
                    //           System.out.println("i = "+i);
                    if (curr.e[i] != null && k.CompareTo(curr.e[i].key) < 0) {
                        //              System.out.println("Search subtree "+i+" of node " + curr);

                        if (print)
                            Console.WriteLine(" ---- traverse LEFT subtree of " + curr.e[i].toString());

                        curr = curr.child[i];
                        break;                 // end to for
                    }

                    if (curr.e[i] != null && k.CompareTo(curr.e[i].key) == 0) {
                        if (print)
                            Console.WriteLine(" ---- FOUND: " + curr.e[i].toString());

                        return (curr.e[i]);
                    }

                    if (i == 2 || curr.e[i + 1] == null) // e[i] is last value key
                    {
                        //              System.out.println("Search subtree "+i+" of node " + curr);

                        if (print)
                            Console.WriteLine(" ---- traverse RIGHT subtree of " + curr.e[i].toString());

                        curr = curr.child[i + 1];
                        break;                 // end to for
                    }
                }

            }

            if (print)
                Console.WriteLine("\n===== Not found... Search ended at node: " + (searchEndPos == null ? "null" : searchEndPos.toString()));

            //     System.out.println("exit keySearch(" + k + ")");
            return (null);   // k not found
        }


        /* ================================================
           get(k): return value associated with key k
           ================================================ */
        public object get(String k) {
            Entry e;

            e = keySearch(k);

            if (e == null)
                return null;
            else
                return (e.value);
        }


        /* ================================================
           put(k): insert (k,v)
           ================================================ */
        public object put(String k, object v) {
            Node p;
            Entry e;


            /* ------------------------
           Special case: empty tree
               ------------------------ */
            if (root == null) {
                p = new Node();

                p.e[0] = new Entry(k, v);

                root = p;
                p.parent = p;          // Parent(root) == root

                return null;
            }

            /* ------------------------
           Other cases
               ------------------------ */
            e = keySearch(k);
            // keySearch sets "searchEndPos = last node visited

            //     System.out.println("Last node visited = "+searchEndPos);

            /* ------------------------
           key found, update value
               ------------------------ */
            if (e != null && k.CompareTo(e.key) == 0) {
                object oldValue;

                Console.Write("*** Update value of " + k + " in entry " + e.toString());

                oldValue = e.value;
                e.value = v;

                return (oldValue);
            }

            /* ------------------------------------------
           key not found:

                   insert (k,v) in node "searchEndPos"
               ------------------------------------------ */
            e = new Entry(k, v);
            insertEntryInThisNode(e, null, searchEndPos);

            return null;
        }


        /* =======================================================
           insertEntryInThisNode(e, rightSubTree, p):

              1. insert "e" and its "rightSubTree" in node p
          2. if over flow, split and insert in parent
           ======================================================= */
        public void insertEntryInThisNode(Entry e, Node rightSubTree, Node p) {
            int i;
            Entry n;
            Node q;
            /*
                   System.out.println("*** Insert entry " + e + ", R subtree " 
                        + rightSubTree + " in node " + p);
            */
            if (p.e[2] == null) {
                /* -----------------
                   There is space
                   ----------------- */
                insertEntryDirectlyInNode(e, rightSubTree, p);
            } else {
                /* --------------------------------
                   There is no more space ....
                   -------------------------------- */
                /*
                          System.out.print("*** Split insert !");
                */

                /* ==================================
               Make a virtual node with 4 keys
               ================================== */
                Entry[] x_e = new Entry[4];
                Node[] x_child = new Node[5];

                x_child[0] = p.child[0];

                i = 0;
                while (i < 3 && p.e[i].key.CompareTo(e.key) < 0) {
                    x_e[i] = p.e[i];
                    x_child[i + 1] = p.child[i + 1];

                    i++;
                }

                /* ----------------------
               Insert e at x_e[i]
               ---------------------- */
                x_e[i] = e;
                x_child[i + 1] = rightSubTree;

                while (i < 3) {
                    x_e[i + 1] = p.e[i];
                    x_child[i + 2] = p.child[i + 1];

                    i++;
                }
                /*
                          System.out.print(" ---- Transitional node = ");
                          for (i=0; i<4; i++)
                          {
                             if ( i != 2 )
                                System.out.print( x_e[i].key + " ");
                             else
                                System.out.print( "(" + x_e[i].key + ") ");
                          }
                          System.out.println();
                */
                /* ==================
                   Distribute keys
                   ================== */

                p.child[0] = x_child[0];
                p.e[0] = x_e[0];
                p.child[1] = x_child[1];
                p.e[1] = x_e[1];
                p.child[2] = x_child[2];
                p.e[2] = null;
                p.child[3] = null;

                n = x_e[2];

                q = new Node();

                q.child[0] = x_child[3];
                q.e[0] = x_e[3];
                q.child[1] = x_child[4];

                if (q.child[0] != null)     // Very sneaky bug found !!!
                    q.child[0].parent = q;

                if (q.child[1] != null)
                    q.child[1].parent = q;

                if (p == root) {
                    /* Split root */
                    Node newRoot = new Node();

                    newRoot.child[0] = p;
                    newRoot.e[0] = n;
                    newRoot.child[1] = q;

                    p.parent = newRoot;
                    q.parent = newRoot;

                    root = newRoot;
                } else {
                    q.parent = p.parent;
                    insertEntryInThisNode(n, q, p.parent);
                }
            }
        }


        /* ==============================================================
           insertEntryDirectlyInNode(e, rightChild, p):

              Node p:                   e[2] == null for sure

                 child[0]  e[0]  child[1]  e[1]  child[2]  e[2]  child[3]

            Let i = the proper entry

            Insert e in the node p (with rightChild, being e's Right child
            ============================================================== */

        public void insertEntryDirectlyInNode(Entry e, Node rightChild, Node p) {
            int i;
            /*
                   System.out.println("DIRECT Insert entry " + e + ", R subtree " 
                        + rightChild + " in node " + p);
            */
            /* ---------------------------------------------------------------
               Node p:

                  child[0]  e[0]  child[1]  e[1]  child[2]  e[2]  child[3]

               e[2] == null for sure

               **** We need to find the spot to insert e
               --------------------------------------------------------------- */

            if (p.e[0] == null ||
                 (p.e[0] != null && e.key.CompareTo(p.e[0].key) < 0)) {
                // e goes in e[0]
                for (i = 2; i > 0; i--) {
                    p.e[i] = p.e[i - 1];
                    p.child[i + 1] = p.child[i];
                }
                p.e[0] = e;
                p.child[1] = rightChild;
            } else if ((p.e[1] == null) ||
                    (p.e[1] != null && e.key.CompareTo(p.e[1].key) < 0)) {
                // e goes in e[1]
                for (i = 2; i > 1; i--) {
                    p.e[i] = p.e[i - 1];
                    p.child[i + 1] = p.child[i];
                }
                p.e[1] = e;
                p.child[2] = rightChild;
            } else {
                // e goes in e[2]
                p.e[2] = e;
                p.child[3] = rightChild;
            }

            /* ========================================
           Fix parent link
           ======================================== */
            if (rightChild != null) {
                rightChild.parent = p;
            }
            /*
                   System.out.println("Result: " + p);
            */
        }




        /* =================================================
           remove(k)
           ================================================= */
        public object remove(String k) {
            Entry e;
            Node p;
            int pos;

            /* -----------------------
               Find k to remove
           ----------------------- */
            e = keySearch(k);

            /* =======================
               Check if k exists...
           ======================= */
            if (e == null) {
                return (null);      // Not found, nothing to delete...
            }

            Entry old = e;      // Save for return value

            /* ----------------------------------------------------
               Note:

              searchEndPos = node containing entry e
           ---------------------------------------------------- */
            p = searchEndPos;


            /* ----------------------------------------------------
               Find position of e inside p:

                  p --> | T0 | e0 | T1 | e1 | T2 | e2 | T3 |

               Which of the e?  is  e ????
           ---------------------------------------------------- */
            for (pos = 0; pos < 3; pos++)
                if (p.e[pos] == e)
                    break;

            /* ----------------------------------------------------
           Delete entry e from 2,4-tree
           ---------------------------------------------------- */
            if (p.child[0] == null /* => leaf node */ ) {
                /* ==================================================
                   Delete entry e from node

                   Note: this is done by MOVING right most entries
                     into the slot containing e !!!
                   ================================================== */
                for (int i = pos; i < 2; i++)
                    p.e[i] = p.e[i + 1];
                p.e[2] = null;      // The right most entry is for sure null !

            } else {
                /* ------------------------------------------------
               Non-leaf: replace e with e's successor
               ------------------------------------------------ */
                Node q;

                /* ------------------------------------------------
               Use q to traverse to p's successor
               ------------------------------------------------ */
                Console.WriteLine("pos = " + pos);
                q = p.child[pos + 1];   // Go right

                while (q.child[0] != null)
                    q = q.child[0]; // Go left all the way down.

                /* ------------------------------------------------
               Delete e
               ------------------------------------------------ */
                Console.WriteLine("Replace " + p.e[pos].toString() + " with: " + q.e[0].toString());

                p.e[pos] = q.e[0];  // Replace e by e's successor

                for (int i = 0; i < 2; i++) // Delete e's successor in q
                    q.e[i] = q.e[i + 1];
                q.e[2] = null;

                /* -----------------------------------------------------
                   The target entry is now the successor's location
                   ----------------------------------------------------- */
                p = q;      // node where entry was deleted

                Console.WriteLine("Result: p = " + p == null ? "null" : p.toString());
            }


            /* ***********************************************
               Check for a possible underflow situation
           *********************************************** */
            if (p.e[0] == null) {
                handleUnderflow(p, null);  // Node p is empty
                                           // Second parameter is a subtree
                                           // hung under p
            }

            return old.value;       // Return old value
        }


        /* ====================================================================
           handleUnderflow(p, Z):

              Handle underflow in node p (i.e., p is empty !!!)

              Z = the subtree that was attached BELOW p BEFORE p became empty

           How to attach Z:

          if root is split, Z is the new root

          In transfer with RIGHT sibling,  Z is child[0]
          In transfer with LIFT  sibling,  Z is child[1]

          In MERGE with RIGHT sibling,  Z is child[0]
          In MERGE with LIFT  sibling,  Z is child[2] !!!
           ==================================================================== */
        public void handleUnderflow(Node p, Node Z) {
            /* ================================================
           Check if the root node is empty

           Note: This is a base case of the recursion....
               ================================================ */
            if (p == root) {
                root = Z;       // Delete the empty root node p
                return;
            }

            /* -------------------------------------------------------------
               Find the position of p inside p's parent node

                  p's parent --> | T0 | e0 | T1 | e1 | T2 | e2 | T3 |
                       ^         ^         ^         ^
                       | 	       |         |         |
                     pos=0     pos=1     pos=2     pos=3

               Which of the T?  is  p ????  (pos = 0, 1, 2, or 3)
               ---------------------------------------------------- */
            Node parent;
            int pos;

            parent = p.parent;

            for (pos = 0; pos < 4; pos++) {
                if (parent.child[pos] == p)
                    break;
            }

            Console.WriteLine("\n========== handle underflow in:");
            Console.WriteLine("p = " + p == null ? "null" : p.toString());
            Console.WriteLine("parent = " + parent == null ? "null" : parent.toString());
            Console.WriteLine("pos = " + pos);
            Console.WriteLine("Subtree Z = " + Z);

            /* -------------------------------------------------------------
               TRY a TRANSFER with your RIGHT sibling
           ------------------------------------------------------------- */
            if ((pos <= 2 && parent.child[pos + 1] != null) /* p has a right sibling */
                 && parent.child[pos + 1].e[1] != null /* R sibling has >= 2 entries */) {
                /* -----------------------------------------------------------
                   Legend to understand the transfer operation: (assume pos=1)

                                 pos	  pos+1
                                  |           |
                                  V           V
                           parent --> | T0 | e0 | T1 | *e1* | T2 | e2 | T3 |
                                                  |           |
                                  p	  R_sibling

                   Entry sandwiched between pos and pos+1 is moved into p !!!
                   ----------------------------------------------------------- */
                Node R_sibling = parent.child[pos + 1];
                /*
                     System.out.println("Underflow !!!! ===> Transfer with R sibling " 
                                + R_sibling);
                */
                p.child[0] = Z;         // Hang Z
                if (Z != null) Z.parent = p;

                p.e[0] = parent.e[pos];                // Transfer parent's entry
                p.child[1] = R_sibling.child[0];    // Transfer sibling's subtree
                if (p.child[1] != null)     // *** Update PARENT ref !! ***
                    p.child[1].parent = p;

                parent.e[pos] = R_sibling.e[0]; // Move sibling entry to parent

                /* ===================================
                   Delete e[0] in R_sibling
                   =================================== */
                for (int i = 0; i < 2; i++)  // ... accomplish by shifting
                {
                    R_sibling.e[i] = R_sibling.e[i + 1];
                    R_sibling.child[i] = R_sibling.child[i + 1];
                }
                R_sibling.e[2] = null;          // e[2] is now empty
                R_sibling.child[3] = null;      // child[3] is now empty

                return; // Done
            }

            /* -------------------------------------------------------------
               TRY a TRANSFER with your LEFT sibling
           ------------------------------------------------------------- */
            else if (pos > 0             /* p has a left sibling */
                 && parent.child[pos - 1].e[1] != null /* L sibling has >= 2 entries */) {
                /* -----------------------------------------------------------
                   Legend to understand the transfer operation: (assume pos=1)

                           pos-1       pos	  
                             |	         |         
                             V	         V        
                           parent --> | T0 | *e0* | T1 | e1 | T2 | e2 | T3 |
                                         |          |       
                           L_sibling        p

                   Entry sandwiched between pos-1 and pos is moved into p !!!
                   ----------------------------------------------------------- */
                Node L_sibling = parent.child[pos - 1];
                int last;
                /*
                     System.out.println("Underflow !!!! ===> Transfer with L sibling " 
                                + L_sibling);
                */
                /* -------------------------------------
                   Find the last entry in L_sibling
                   ------------------------------------- */
                for (last = 0; last < 3; last++)
                    if (L_sibling.e[last] == null)
                        break;

                if (last >= 3 || L_sibling.e[last] == null)
                    last--;
                /*
                     System.out.println("*** last = " + last);
                */
                p.child[0] = L_sibling.child[last + 1]; // Transfer sibling's subtree
                if (p.child[0] != null)     // *** Update PARENT ref !! ***
                    p.child[0].parent = p;

                p.e[0] = parent.e[pos - 1];              // Transfer parent's entry

                p.child[1] = Z;         // Hang Z
                if (Z != null) Z.parent = p;

                parent.e[pos - 1] = L_sibling.e[last];  // Move sibling entry to parent

                /* ===================================
                   Delete e[lst] in L_sibling
                   =================================== */
                L_sibling.e[last] = null;
                L_sibling.child[last + 1] = null;

                return; // Done
            }

            /* ========================================================
           We MUST use a merge operation

           Legend of the position of p inside p's parent node

                  p's parent --> | T0 | e0 | T1 | e1 | T2 | e2 | T3 |
                                   ^         ^         ^         ^
                                   |         |         |         |
                                 pos=0     pos=1     pos=2     pos=3
               ======================================================== */

            /* -------------------------------------------------------------
               TRY a MERGE with your RIGHT sibling
           ------------------------------------------------------------- */
            else if (pos != 3 /* No Right sibling possible */
                  && parent.child[pos + 1] != null /* There is a right sibling */) {
                /* -----------------------------------------------------------
                   Legend to understand the merge operation: (assume pos=1)

                                             pos         pos+1
                                              |           |
                                              V           V
                       parent --> | T0 | e0 | T1 | *e1* | T2 | e2 | T3 |
                                              |           |
                                              p          R_sibling

                   Entry sandwiched between pos and pos+1 is moved into p !!!
                   ----------------------------------------------------------- */
                Node R_sibling = parent.child[pos + 1];  // R_sibling has ONLY 1 entry !

                Console.WriteLine("Underflow !!!! ===> MERGE with R sibling " + (R_sibling == null ? "null" : R_sibling.toString()));

                p.child[0] = Z;         // Hang Z
                if (Z != null) Z.parent = p;

                p.e[0] = parent.e[pos];                // Transfer parent's entry

                p.child[1] = R_sibling.child[0];       // Transfer sibling's subtree
                if (p.child[1] != null)
                    p.child[1].parent = p;

                p.e[1] = R_sibling.e[0];        // Transfer sibling (ONLY) entry

                p.child[2] = R_sibling.child[1];       // Transfer sibling's subtree
                if (p.child[2] != null)
                    p.child[2].parent = p;

                /* ======================================
                   Delete parent.e[pos] in parent node
                   ====================================== */
                for (int i = pos; i < 2; i++) {
                    parent.e[i] = parent.e[i + 1];
                    parent.child[i + 1] = parent.child[i + 2];
                }
                parent.e[2] = null;
                parent.child[3] = null;

                if (parent.e[0] == null)
                    handleUnderflow(parent, p);
            } else // pos == 3, we must merge LEFT ...
              {
                /* -----------------------------------------------------------
                   Legend to understand the transfer operation: (assume pos=1)

                                   pos-1       pos
                                     |          |
                                     V          V
                       parent --> | T0 | *e0* | T1 | e1 | T2 | e2 | T3 |
                                     |          |
                               L_sibling        p

                   Entry sandwiched between pos-1 and pos is moved into p !!!
                   ----------------------------------------------------------- */
                Node L_sibling = parent.child[pos - 1]; // L_sibling has 1 entry !

                Console.WriteLine("Underflow !!!! ===> MERGE with L sibling " + (L_sibling == null ? "null" : L_sibling.toString()));

                L_sibling.e[1] = parent.e[pos - 1];     // Transfer parent's entry
                L_sibling.child[2] = Z;     // Hang Z
                if (Z != null) Z.parent = p;

                /* ======================================
                   Delete parent.e[pos] in parent node
                   ====================================== */
                for (int i = pos - 1; i < 2; i++) {
                    parent.e[i] = parent.e[i + 1];
                    parent.child[i + 1] = parent.child[i + 2];
                }
                parent.e[2] = null;
                parent.child[3] = null;

                if (parent.e[0] == null)
                    handleUnderflow(parent, L_sibling);
            }
        }

        /* =================================================
           printTree()
           ================================================= */

        int MaxLevel;

        void padding(String s, int n) {
            int i;

            for (i = 0; i < n; i++)
                Console.Write(s);
        }


        void printSub(Node p, int id, int level) {
            if (level > MaxLevel)
                MaxLevel = level;

            int i;

            if (p == null)
                return;


            if (p.child[3] != null) {
                printSub(p.child[3], 3, level + 1);
            }

            if (p.child[2] != null) {
                printSub(p.child[2], 2, level + 1);
            }

            if (p.child[2] != null) {
                Console.Write("|"); padding("--", level);
                Console.WriteLine("numero hijo " + id + ":" + p.toString());


                if (id == 0 && level == MaxLevel)
                    Console.WriteLine();

                if (p.child[1] != null) {
                    printSub(p.child[1], 1, level + 1);
                }
            } else {
                if (p.child[1] != null) {
                    printSub(p.child[1], 1, level + 1);
                }

                Console.Write("|"); padding("--", level);
                Console.WriteLine("numero hijo " + id + ":" + p.toString());


                if (id == 0 && level == MaxLevel)
                    Console.WriteLine();
            }

            if (p.child[0] != null) {
                printSub(p.child[0], 0, level + 1);
                //        System.out.println();
            }
        }

        public void printTree() {
            MaxLevel = 0;
            Console.WriteLine();
            printSub(root, 0, 0);
        }

        List<object> ToListDerechoSub(Node p, List<object> Lista) {
            if (p != null) {
                // Agrega a la lista los nodos que no son nulos

              if (p.child[3] != null) {
                Lista = ToListDerechoSub(p.child[3], Lista);
              }

              if (p.e[2] != null) {
                Lista.Add(p.e[2].value);
              }

              if (p.child[2] != null) {
                Lista = ToListDerechoSub(p.child[2], Lista);
              }

              if (p.e[1] != null) {
                Lista.Add(p.e[1].value);
              }
              
              if (p.child[1] != null) {
                Lista = ToListDerechoSub(p.child[1], Lista);
              }

              if (p.e[0] != null) {
                Lista.Add(p.e[0].value);
              }

              if (p.child[0] != null) {
                Lista = ToListDerechoSub(p.child[0], Lista);
              }
            }
            return Lista;
        }

        List<object> ToListIzquierdoSub(Node p, List<object> Lista) {
            if (p != null) {
                // Agrega a la lista los nodos que no son nulos

                if (p.child[0] != null) {
                    Lista = ToListIzquierdoSub(p.child[0], Lista);
                }

                if (p.e[0] != null) {
                    Lista.Add(p.e[0].value);
                }

                if (p.child[1] != null) {
                    Lista = ToListIzquierdoSub(p.child[1], Lista);
                }

                if (p.e[1] != null) {
                    Lista.Add(p.e[1].value);
                }

                if (p.child[2] != null) {
                    Lista = ToListIzquierdoSub(p.child[2], Lista);
                }

                if (p.e[2] != null) {
                    Lista.Add(p.e[2].value);
                }

                if (p.child[3] != null) {
                    Lista = ToListIzquierdoSub(p.child[3], Lista);
                }
            }
            return Lista;
        }

        public List<object> ToList(string recorrido) {
            List<object> Lista = new List<object>();
            if (recorrido != null && recorrido.ToLower() == "derecho") {
                Lista = ToListDerechoSub(root, Lista);
            } else {
                Lista = ToListIzquierdoSub(root, Lista);
            }
            
            return Lista;
        }

        public bool error;

        public void checkSub(Node p) {
            int i;

            if (p == null)
                return;

            // Check parent child relationship for node
            for (i = 0; i < 4; i++)
                if (p.child[i] != null) {
                    if (p.child[i].parent != p) {
                        printTree();
                        Console.WriteLine("---------------------------");
                        Console.WriteLine("Error:");
                        Console.WriteLine("p: " + (p == null ? "null" : p.toString()));
                        Console.WriteLine("p.child[" + i + "] = " + p.child[i].toString());
                        Console.WriteLine("p.child[" + i + "].parent = " + (p.child[i].parent == null ? "null" : p.child[i].parent.toString()));
                    }
                }

            // Recurse
            for (i = 0; i < 4; i++)
                if (p.child[i] != null)
                    checkSub(p.child[i]);
        }

        public void checkTree() {
            error = false;
            checkSub(root);
            if (error)
                Console.ReadLine();
        }

    }

}

