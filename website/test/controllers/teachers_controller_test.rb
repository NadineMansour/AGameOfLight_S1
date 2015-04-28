require 'test_helper'


class TeachersControllerTest < ActionController::TestCase
  include Devise::TestHelpers


  def setup
    @request.env["devise.mapping"] = Devise.mappings[:teacher]
    sign_in teachers(:one)
  end


  test "should get view game records" do
    get( :view_game_records)
    assert_response :success
    assert_not_nil assigns(:current_teacher)
    assert_not_nil assigns(:students)
    assert_template :view_game_records
    assert_template layout: "layouts/application"
  end


   test "should get view verified students" do
    # start the request
    get( :view_school_verified_students)
    assert_response :success
    assert_not_nil assigns(:current_teacher)
    assert_not_nil assigns(:students)
    assert_nil assigns(:method)
    assert_nil assigns(:order)
    # we have only 2 verified students in the fixtures
    assert_equal 2, assigns(:students).count
    assert_template :view_school_verified_students
    assert_template layout: "layouts/application"
  end


  test "teacher can view verified students and filter" do
    get( :view_school_verified_students, {'filter_method' => '1', 'search' => '5'})
    assert_response :success
    assert_not_nil assigns(:students)
    assert_not_nil assigns(:current_teacher)
    assert_not_nil assigns(:method)
    assert_not_nil assigns(:search)
    assert_equal 1, assigns(:students).count
    assert_equal "5", assigns(:students).first.grade
    assert_template :view_school_verified_students
    assert_template layout: "layouts/application"
  end


  test "teacher can view verified students and filter without result" do
    get( :view_school_verified_students, {'filter_method' => '1', 'search' => '50'})
    assert_response :success
    assert_not_nil assigns(:students)
    assert_not_nil assigns(:current_teacher)
    assert_not_nil assigns(:method)
    assert_not_nil assigns(:search)
    assert_equal 0, assigns(:students).count
    assert_template :view_school_verified_students
    assert_template layout: "layouts/application"
  end
  

end