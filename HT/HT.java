import java.util.Scanner;

public class HT {
    public static void main(String[] args) {
        Scanner scanner = new Scanner(System.in);
        int n = 4;

        double[][] A = new double[n][n];
        double[] B = new double[n];

        System.out.println("Enter the coefficients of matrix A (with “+n+” in each row):");
        for (int i = 0; i < n; i++) {
            for (int j = 0; j < n; j++) {
                A[i][j] = scanner.nextDouble();
            }
        }

        System.out.println("Enter the vector of free terms B:");
        for (int i = 0; i < n; i++) {
            B[i] = scanner.nextDouble();
        }

        System.out.println("\n--- Initial system of equations (A * X = B) ---");
        printSystem(A, B);

        double[][] LU = new double[n][n];
        for (int i = 0; i < n; i++) {
            System.arraycopy(A[i], 0, LU[i], 0, n);
        }
        int[] P = new int[n];
        for (int i = 0; i < n; i++) P[i] = i;

        if (!decomposeLUP(LU, P, n)) {
            System.out.println("The matrix is singular. There is no solution, or there are infinitely many solutions.");
            return;
        }

        printLUP(LU, P, n);

        double[] X = solveLUP(LU, P, B, n);

        System.out.println("\n--- The vector of unknowns (X) ---");
        for (int i = 0; i < n; i++) {
            System.out.printf("x%d = %.4f\n", (i + 1), X[i]);
        }
        scanner.close();
    }

    private static boolean decomposeLUP(double[][] LU, int[] P, int n) {
        for (int i = 0; i < n; i++) {
            double maxA = 0.0;
            int imax = i;
            for (int k = i; k < n; k++) {
                double absA = Math.abs(LU[k][i]);
                if (absA > maxA) {
                    maxA = absA;
                    imax = k;
                }
            }

            if (maxA < 1e-9) return false;

            if (imax != i) {
                int tempP = P[i]; P[i] = P[imax]; P[imax] = tempP;
                double[] tempLU = LU[i]; LU[i] = LU[imax]; LU[imax] = tempLU;
            }

            for (int j = i + 1; j < n; j++) {
                LU[j][i] /= LU[i][i];
                for (int k = i + 1; k < n; k++) {
                    LU[j][k] -= LU[j][i] * LU[i][k];
                }
            }
        }
        return true;
    }

    private static double[] solveLUP(double[][] LU, int[] P, double[] B, int n) {
        double[] x = new double[n];
        double[] y = new double[n];

        for (int i = 0; i < n; i++) {
            y[i] = B[P[i]];
            for (int j = 0; j < i; j++) {
                y[i] -= LU[i][j] * y[j];
            }
        }

        for (int i = n - 1; i >= 0; i--) {
            x[i] = y[i];
            for (int j = i + 1; j < n; j++) {
                x[i] -= LU[i][j] * x[j];
            }
            x[i] /= LU[i][i];
        }
        return x;
    }

    private static void printSystem(double[][] A, double[] B) {
        for (int i = 0; i < A.length; i++) {
            for (int j = 0; j < A[i].length; j++) {
                System.out.printf("%8.2f*x%d ", A[i][j], j + 1);
                if (j < A[i].length - 1) System.out.print("+ ");
            }
            System.out.printf("= %8.2f\n", B[i]);
        }
    }

    private static void printLUP(double[][] LU, int[] P, int n) {
        System.out.println("\n--- Matrix P (Permutation) ---");
        for (int i = 0; i < n; i++) {
            for (int j = 0; j < n; j++) {
                System.out.print((P[i] == j ? 1 : 0) + "\t");
            }
            System.out.println();
        }

        System.out.println("\n--- Matrix L (Lower triangular) ---");
        for (int i = 0; i < n; i++) {
            for (int j = 0; j < n; j++) {
                if (i > j) System.out.printf("%8.4f ", LU[i][j]);
                else if (i == j) System.out.printf("%8.4f ", 1.0);
                else System.out.printf("%8.4f ", 0.0);
            }
            System.out.println();
        }

        System.out.println("\n--- U matrix (Upper triangular) ---");
        for (int i = 0; i < n; i++) {
            for (int j = 0; j < n; j++) {
                if (i <= j) System.out.printf("%8.4f ", LU[i][j]);
                else System.out.printf("%8.4f ", 0.0);
            }
            System.out.println();
        }
    }
}