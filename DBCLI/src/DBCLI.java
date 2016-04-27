import com.firebase.client.Firebase;
import com.firebase.client.FirebaseError;

import java.io.BufferedWriter;
import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.io.PrintWriter;

import java.text.DateFormat;
import java.text.SimpleDateFormat;

import java.util.Date;
import java.util.HashMap;
import java.util.Map;
import java.util.concurrent.Semaphore;

public class DBCLI {

    private static Firebase DATABASE;
    private static Firebase USER_TABLE;
    private static Firebase MOVIE_TABLE;

    private static String MODE_PARAM;

    private static final File LOG = new File("log.txt");
    private static FileWriter LOG_FW;
    private static BufferedWriter LOG_BW;
    private static PrintWriter LOG_PW;
    private static final DateFormat LOG_DATE_FORMAT =
            new SimpleDateFormat("yyyy/MM/dd HH:mm:ss");

    private static final Semaphore sem = new Semaphore(0);

    public static void main(String[] args) {
        // Initialize logging
        try {
            LOG_FW = new FileWriter(LOG, true);
            LOG_BW = new BufferedWriter(LOG_FW);
            LOG_PW = new PrintWriter(LOG_BW);
            LOG_PW.println("\n\nBegin logging");
        } catch (IOException e) {
            e.printStackTrace();
        }

        DATABASE = new Firebase("https://movierecommender.firebaseio.com/");
        USER_TABLE = DATABASE.child("users");
        MOVIE_TABLE = DATABASE.child("movies");

        // Arguments and mode checking
        if (args.length < 2) {
            printArgError(args);
        } else {
            MODE_PARAM = args[0];

            switch (MODE_PARAM) {
                case "addUser": {
                    if (args.length < 8) {
                        printArgError(args);
                    }

                    String description = args[1];
                    String email = args[2];
                    String major = args[3];
                    String name = args[4];
                    String passwordHash = args[5];
                    String status = args[6];
                    String username = args[7];

                    Map<String, String> mongoose = new HashMap<>();
                    mongoose.put("description", description);
                    mongoose.put("email", email);
                    mongoose.put("major", major);
                    mongoose.put("name", name);
                    mongoose.put("passwordHash", passwordHash);
                    mongoose.put("status", status);
                    mongoose.put("username", username);

                    USER_TABLE.child(username).setValue(mongoose, new Firebase.CompletionListener() {
                        @Override
                        public void onComplete(FirebaseError firebaseError, Firebase firebase) {
                            sem.release();
                            System.exit(0);
                        }
                    });

                    try {
                        sem.acquire();
                    } catch (InterruptedException e) {
                        e.printStackTrace();
                    }
                } break; case "addNewMovie": {
                    if (args.length < 4) {
                        printArgError(args);
                    }

                    String averageRating = args[1];
                    String imgURL = args[2];
                    String title = args[3];

                    Map<String, String> mongoose = new HashMap<>();
                    mongoose.put("averageRating", averageRating);
                    mongoose.put("imgURL", imgURL);
                    mongoose.put("title", title);

                    MOVIE_TABLE.child(title).setValue(mongoose, new Firebase.CompletionListener() {
                        @Override
                        public void onComplete(FirebaseError firebaseError, Firebase firebase) {
                            sem.release();
                            System.exit(0);
                        }
                    });

                    try {
                        sem.acquire();
                    } catch (InterruptedException e) {
                        e.printStackTrace();
                    }
                } break; case "addRating": {
                    if (args.length < 6) {
                        printArgError(args);
                    }

                    String title = args[1];
                    String averageRating = args[2];
                    String comment = args[3];
                    String rating = args[4];
                    String user = args[5];

                    Map<String, String> mongoose = new HashMap<>();
                    mongoose.put("comment", comment);
                    mongoose.put("rating", rating);
                    mongoose.put("user", user);

                    Map<String, Object> mongoose2 = new HashMap<>();
                    mongoose2.put("averageRating", averageRating);

                    Firebase tempRef = MOVIE_TABLE.child(title);

                    tempRef.updateChildren(mongoose2);
                    tempRef.child("ratings").push().setValue(mongoose, new Firebase.CompletionListener() {
                        @Override
                        public void onComplete(FirebaseError firebaseError, Firebase firebase) {
                            sem.release();
                            System.exit(0);
                        }
                    });

                    try {
                        sem.acquire();
                    } catch (InterruptedException e) {
                        e.printStackTrace();
                    }
                } break; case "uset": {
                    if (args.length < 4) {
                        printArgError(args);
                    }

                    String path = args[1];
                    String field = args[2];
                    String value = args[3];

                    Map<String, Object> mongoose = new HashMap<>();
                    mongoose.put(field, value);

                    DATABASE.child(path).updateChildren(mongoose, new Firebase.CompletionListener() {
                        @Override
                        public void onComplete(FirebaseError firebaseError, Firebase firebase) {
                            sem.release();
                            System.exit(0);
                        }
                    });

                    try {
                        sem.acquire();
                    } catch (InterruptedException e) {
                        e.printStackTrace();
                    }
                } break; default: {
                    Date date = new Date();
                    StringBuffer temp = new StringBuffer();
                    temp.append(LOG_DATE_FORMAT.format(date));
                    temp.append("   Invalid mode argument: ");
                    temp.append(args[0]);
                    LOG_PW.println(temp);
                    LOG_PW.close();
                } break;
            }
        }
    }

    private static void printArgError(String[] args) {
        Date date = new Date();
        StringBuffer temp = new StringBuffer();
        temp.append(LOG_DATE_FORMAT.format(date));
        temp.append("   Too few arguments: ");
        temp.append(args.length);
        LOG_PW.println(temp);
        LOG_PW.close();
    }
}
